using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class MoviesController : Controller
    {
        [Route("{controller}/{id}")]
        public async Task<IActionResult> Index(int id, string lang)
        {
            ViewBag.Lang = (lang is null) ? "ukr" : lang;
            return View();
        }
    }
}
