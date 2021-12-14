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
        public async Task<IActionResult> Register(string lang, RegisterViewModel model)
        {
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
            var emailChecker = new EmailAddressAttribute();
            if (string.IsNullOrEmpty(model.Email) || !emailChecker.IsValid(model.Email))
            {
                ModelState.AddModelError("WrongEmail", "Not a valid or empty email");
                return RedirectToAction("Register", "Account", model);
            }
            if (string.IsNullOrEmpty(model.FirstName))
            {
                ModelState.AddModelError("NoFirstName", "First name cannot be empty");
                return RedirectToAction("Register", "Account", model);
            }
            if (string.IsNullOrEmpty(model.LastName))
            {
                ModelState.AddModelError("NoLastName", "Last name cannot be empty");
                return RedirectToAction("Register", "Account", model);
            }
            if (!(model.PhoneNumber is null) && (model.PhoneNumber.Length != 12 || !model.PhoneNumber.All(char.IsDigit)
                || !model.PhoneNumber.StartsWith("38")))
            {
                ModelState.AddModelError("WrongPhoneNumber", "Phone number is not in format 38XXXXXXXXXX");
                return RedirectToAction("Register", "Account", model);
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
                // To cookies!!!
                var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (signInResult.Succeeded)
                {
                    GenreModel genre;
                    foreach (var genreId in model.Genres)
                    {
                        genre = await _genreService.GetByIdAsync(genreId);
                        try
                        {
                            await _genreService.AddUserToGenreAsync(genre, UserId);
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
            return RedirectToAction("Register", "Account", model);
        }
    }
}
