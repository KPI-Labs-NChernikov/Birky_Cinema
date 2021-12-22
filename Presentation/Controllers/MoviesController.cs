using Business.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class MoviesController : BaseController
    {
        private readonly IGenreService _genreService;

        private readonly IMovieService _movieService;

        private readonly ISessionService _sessionService;

        private readonly IReviewService _reviewService;

        private readonly IActorService _actorService;

        private readonly ICountryService _countryService;

        private readonly IDirectorService _directorService;

        private readonly IScenarioWriterService _scenarioWriterService;

        private readonly UserManager<User> _userManager;

        public MoviesController(IGenreService genreService, IMovieService movieService, ISessionService sessionService, IReviewService reviewService, IActorService actorService, ICountryService countryService, IDirectorService directorService, IScenarioWriterService scenarioWriterService, UserManager<User> userManager)
        {
            _genreService = genreService;
            _movieService = movieService;
            _sessionService = sessionService;
            _reviewService = reviewService;
            _actorService = actorService;
            _countryService = countryService;
            _directorService = directorService;
            _scenarioWriterService = scenarioWriterService;
            _userManager = userManager;
        }

        [Route("{controller}/{id}")]
        public async Task<IActionResult> Index(int id, string lang)
        {
            AnalyzeLang(lang);
            lang = ViewBag.Lang;
            var movie = await _movieService.GetByIdAsync(id);
            MovieViewModel model;
            if (!(movie is null))
            {
                model = new MovieViewModel()
                {
                    Id = movie.Id,
                    LinkToAffiche = movie.LinkToAffiche.Replace('\\', '/'),
                    LinkToPoster = movie.LinkToPoster.Replace('\\', '/'),
                    Name = _movieService.GetNameForLang(movie, lang) + " (" + movie.Year + ")",
                    Description = _movieService.GetDescriptionForLang(movie, lang),
                    AgeRestriction = movie.AgeRestriction + "+",
                    Rating = movie.Rating,
                    Genre = string.Join(", ", (await _genreService.GetMovieGenresAsync(movie.Id)).Select(m => _genreService.GetNameForLang(m, lang))),
                    Actors = string.Join(", ", (await _actorService.GetMovieActorsAsync(movie.Id)).Select(m => _actorService.GetFirstNameForLang(m, lang) + " " + _actorService.GetLastNameForLang(m, lang))),
                    Authors = string.Join(", ", (await _scenarioWriterService.GetMovieScenarioWritersAsync(movie.Id)).Select(m => _scenarioWriterService.GetFirstNameForLang(m, lang) + " " + _scenarioWriterService.GetLastNameForLang(m, lang))),
                    Director = string.Join(", ", (await _directorService.GetMovieDirectorsAsync(movie.Id)).Select(m => _directorService.GetFirstNameForLang(m, lang) + " " + _directorService.GetLastNameForLang(m, lang))),
                    Country = _countryService.GetNameForLang((await _countryService.GetByIdAsync(movie.CountryId)), lang),
                    Length = movie.Length + " "
                };
                model.Length += lang switch
                {
                    "eng" => "minutes",
                    "ru" => "минут",
                    _ => "хвилин",
                };
                var recommended = (await _movieService.GetSimilarAsync(movie.Id))
                .Select(m => new MovieCarouselViewModel()
                {
                    Id = m.Id,
                    LinkToAffiche = m.LinkToAffiche.Replace('\\', '/'),
                    Name = _movieService.GetNameForLang(m, lang)
                });
                model.Recommended = recommended;
            }
            else
            {
                model = null;
                ModelState.AddModelError("MovieNotFoundError", "Такого фільму немає в базі");
            }
            return View(model);
        }
    }
}
