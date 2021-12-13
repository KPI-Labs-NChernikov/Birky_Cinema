using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IMovieService _movieService;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync(string lang)
        {
            ViewBag.Lang = (lang is null) ? "ukr" : lang;
            var posters = _movieService
                .GetAll()
                .Take(5)
                .Select(m => new MovieReducedViewModel()
                {
                    Id = m.Id,
                    LinkToAffiche = m.LinkToAffiche.Replace('\\', '/'),
                    LinkToPoster = m.LinkToPoster.Replace('\\', '/'),
                    Name = _movieService.GetNameForLang(m, lang) + " (" + m.Year + ")",
                    Description = _movieService.GetDescriptionForLang(m, lang)
                }
                ).ToArray();
            var comingSoon = (await _movieService.GetFeatureFilmsAsync())
                .Select(m => new MovieCarouselViewModel() 
                {
                    Id = m.Id,
                    LinkToAffiche = m.LinkToAffiche.Replace('\\', '/'),
                    Name = _movieService.GetNameForLang(m, lang)
                });
            var shorts = (await _movieService.GetShortsAsync())
                .Select(m => new MovieCarouselViewModel()
                {
                    Id = m.Id,
                    LinkToAffiche = m.LinkToAffiche.Replace('\\', '/'),
                    Name = _movieService.GetNameForLang(m, lang)
                }); ;
            var model = new MainPageViewModel()
            {
                Posters = posters,
                ComingSoon = comingSoon,
                Shorts = shorts
            };
            return View(model);
        }

        [Route("{controller}/InDevelopment")]
        public IActionResult InDevelopment(string lang)
        {
            ViewBag.Lang = (lang is null) ? "ukr" : lang;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
