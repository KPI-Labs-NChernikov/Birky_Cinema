using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly CookieOptions cookieOptions = new CookieOptions()
        {
            MaxAge = new TimeSpan(182, 15, 0, 0)
        };

        protected void AnalyzeLang(string lang)
        {
            var key = "lang";
            if (Request.Cookies.ContainsKey(key))
            {
                if (lang is null)
                    lang = Request.Cookies[key];
                else if (lang != Request.Cookies[key])
                {
                    Response.Cookies.Delete(key);
                    Response.Cookies.Append(key, lang, cookieOptions);
                }
            }
            else
            {
                if (lang is null)
                    lang = "ukr";
                Response.Cookies.Append(key, lang, cookieOptions);
            }
            ViewBag.Lang = lang;
        }

        protected void WriteInfoToCookies(string key, string info)
        {
            if (Request.Cookies.ContainsKey(key))
            {
                if (info != Request.Cookies[key])
                {
                    Response.Cookies.Delete(key);
                    Response.Cookies.Append(key, info, cookieOptions);
                }
            }
            else
            {
                Response.Cookies.Append(key, info, cookieOptions);
            }
        }

        protected string ReadInfoFromCookies(string key)
        {
            return Request.Cookies.ContainsKey(key) ? Request.Cookies[key] : null;
        }

        internal string UserId => !User.Identity.IsAuthenticated
            ? null
            : User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
