using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IGenreService _genreService;

        private readonly SignInManager<User> _signInManager;

        private readonly UserManager<User> _userManager;

        public AccountController(IGenreService genreService, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _genreService = genreService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register(string lang, RegisterViewModel model)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Cabinet", new { lang });
            AnalyzeLang(lang);
            lang = ViewBag.Lang;
            model.AllGenres = _genreService.GetAll().Select(g => new GenreViewModel()
            {
                Id = g.Id,
                Name = _genreService.GetNameForLang(g, lang)
            });
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Cabinet");
            AnalyzeLang(null);
            var lang = ViewBag.Lang;
            model.AllGenres = _genreService.GetAll().Select(g => new GenreViewModel()
            {
                Id = g.Id,
                Name = _genreService.GetNameForLang(g, lang)
            });
            var emailChecker = new EmailAddressAttribute();
            if (string.IsNullOrEmpty(model.Email) || !emailChecker.IsValid(model.Email))
            {
                ModelState.AddModelError("WrongEmail", "Not a valid or empty email");
                return View(model);
            }
            if (string.IsNullOrEmpty(model.FirstName))
            {
                ModelState.AddModelError("NoFirstName", "First name cannot be empty");
                return View(model);
            }
            if (string.IsNullOrEmpty(model.LastName))
            {
                ModelState.AddModelError("NoLastName", "Last name cannot be empty");
                return View(model);
            }
            if (!(model.PhoneNumber is null))
            {
                if (model.PhoneNumber.Length != 12 || !model.PhoneNumber.All(char.IsDigit)
                || !model.PhoneNumber.StartsWith("38"))
                    ModelState.AddModelError("WrongPhoneNumber", "Phone number is not in format 38XXXXXXXXXX");
                else if (_userManager.Users.Select(u => u.PhoneNumber).Contains(model.PhoneNumber))
                    ModelState.AddModelError("WrongPhoneNumber", $"Phone number {model.PhoneNumber} is already taken");
                return View(model);
            }
            var user = new User()
            {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    NormalizedEmail = model.Email.ToUpperInvariant(),
                    UserName = model.Email,
                    NormalizedUserName = model.Email.ToUpperInvariant(),
                    PhoneNumber = model.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Користувач");
                var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (signInResult.Succeeded)
                {
                    GenreModel genre;
                    foreach (var genreId in model.Genres)
                    {
                        genre = await _genreService.GetByIdAsync(genreId);
                        try
                        {
                            await _genreService.AddUserToGenreAsync(genre, user.Id);
                        }
                        catch (Exception) { }
                    }
                    return RedirectToAction("Index", "Cabinet");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("SignInAfterRegistrationError", error.Description);
                }
            }
            else
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("RegistrationError", error.Description);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string lang, LoginViewModel model)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Cabinet", new { lang });
            AnalyzeLang(lang);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Cabinet");
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Cabinet");
                }
                else
                {
                    AnalyzeLang(null);
                    var lang = ViewBag.Lang;
                    string errorMessage;
                    if (lang == "eng")
                    {
                        errorMessage = "E-mail or/and password are not correct";
                    }
                    else if (lang == "ru")
                    {
                        errorMessage = "Неправильная почта и/или пароль";
                    }
                    else
                    {
                        errorMessage = "Неправильна електронна пошта та/або пароль";
                    }
                    ModelState.AddModelError("LoginError", errorMessage);
                }
            }
            model.Password = null;
            return View(model);
        }
    }
}
