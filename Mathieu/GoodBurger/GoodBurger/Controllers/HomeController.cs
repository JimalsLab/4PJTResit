using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using GoodBurger.Services;
using GoodBurger.Context;
using GoodBurger.ViewModels;

namespace GoodBurger.Controllers
{
    public class HomeController : Controller
    {
        private DataRetrievalService service;
        private IndexViewModel ivm;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            service = new DataRetrievalService();
            ivm = new IndexViewModel();

            if (httpContextAccessor.HttpContext.Request.Cookies["sessionCookie"] == null || httpContextAccessor.HttpContext.Request.Cookies["sessionCookie"] == ""|| httpContextAccessor.HttpContext.Request.Cookies["sessionCookie"].Length <= 5 )
            {
                string guid = Guid.NewGuid().ToString();
                CookieOptions session = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1)
                };
                
                httpContextAccessor.HttpContext.Response.Cookies.Append("sessionCookie", guid, session);
                Carts c = new Carts
                {
                    Guid = guid
                };
                if (service.CreateCart(c))
                {
                    ivm.Guid = guid;
                }
            }

        }

        public IActionResult Index()
        {
            return View(ivm);
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        public IActionResult Items()
        {
            return View();
        }

    }
}
