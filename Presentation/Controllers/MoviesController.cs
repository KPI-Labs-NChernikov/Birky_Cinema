using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class MoviesController : BaseController
    {
        [Route("{controller}/{id}")]
        public async Task<IActionResult> Index(int id, string lang)
        {
            AnalyzeLang(lang);
            lang = ViewBag.Lang;
            return View();
        }
    }
}
