using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GoodBurger.Models;
using GoodBurger.DAL;
using GoodBurger.ViewModels;
using Microsoft.AspNetCore.Http;

namespace GoodBurger.Controllers
{
    public class HomeController : Controller
    {
        private UserService userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(UserService us, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            userService = us;
        }

        public IActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            var temp = _httpContextAccessor.HttpContext.Request.Cookies["UserCookie"];
            if (temp != null)
            {
                indexViewModel.user = userService.GetAllUsers().Where(user => user.Id == int.Parse(temp)).FirstOrDefault();
            }
            return View(indexViewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login(string username,string password)
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            Users user = userService.VerifyLogin(username, password);
            if (user == null)
            {
                indexViewModel.user = null;
            }
            else
            {
                indexViewModel.user = user;

                CookieOptions session = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(30)
                };
                _httpContextAccessor.HttpContext.Response.Cookies.Append("UserCookie", user.Id.ToString(), session);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Disconnect()
        {
            string userid = _httpContextAccessor.HttpContext.Request.Cookies["UserCookie"];
            if (userid != null)
            {
                foreach (var cookie in _httpContextAccessor.HttpContext.Request.Cookies.Keys)
                {
                    _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie);
                }
            }
            var temp = _httpContextAccessor.HttpContext.Request.Cookies["UserCookie"];
            return RedirectToAction("Index");
        }
    }
}
