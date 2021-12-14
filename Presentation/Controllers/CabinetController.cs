using Business.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    public class CabinetController : BaseController
    {
        private readonly IGenreService _genreService;

        private readonly IMovieService _movieService;

        private readonly UserManager<User> _userManager;

        public CabinetController(IGenreService genreService, UserManager<User> userManager, IMovieService movieService)
        {
            _genreService = genreService;
            _userManager = userManager;
            _movieService = movieService;
        }

        public async Task<IActionResult> Index(string lang)
        {
            AnalyzeLang(lang);
            lang = ViewBag.Lang;
            var user = await _userManager.GetUserAsync(User);
            var model = new UserViewModel()
            {
                Name = user.FirstName + " " + user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Genres = string.Join(", ", (await _genreService.GetUserGenresAsync(user.Id)).Select(g => _genreService.GetNameForLang(g, lang))),
        };
            var recommended = (await _movieService.GetRecommendedAsync(user.Id))
                .Select(m => new MovieCarouselViewModel()
                {
                    Id = m.Id,
                    LinkToAffiche = m.LinkToAffiche.Replace('\\', '/'),
                    Name = _movieService.GetNameForLang(m, lang)
                });
            model.Recommended = recommended;
            return View(model);
        }
    }
}
