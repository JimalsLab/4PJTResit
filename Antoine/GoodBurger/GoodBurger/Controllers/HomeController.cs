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
        private ProductService productService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(ProductService ps, UserService us, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            userService = us;
            productService = ps;
        }

        public IActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            var temp = _httpContextAccessor.HttpContext.Request.Cookies["UserCookie"];
            if (temp != null)
            {
                indexViewModel.user = userService.GetAllUsers().Where(user => user.Id == int.Parse(temp)).FirstOrDefault();
            }
            indexViewModel.products = productService.GetAllProducts().Where(p=>p.IdUser == -1).ToList();
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
            return RedirectToAction("Index");
        }

        public IActionResult AddToCart(string id, string number)
        {
            Products product = productService.GetAllProducts().Where(p => p.Id == int.Parse(id)).FirstOrDefault(); 
            if (product.Number < int.Parse(number))
            {
                return RedirectToAction("Index");
            }
            else
            {
                CookieOptions session = new CookieOptions
                {
                    Expires = DateTime.Now.AddHours(3)
                };
                _httpContextAccessor.HttpContext.Response.Cookies.Append(id, number, session);
                return RedirectToAction("Index");
            }
        }

        public IActionResult Cart()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            var temp = _httpContextAccessor.HttpContext.Request.Cookies["UserCookie"];
            if (temp != null)
            {
                indexViewModel.user = userService.GetAllUsers().Where(user => user.Id == int.Parse(temp)).FirstOrDefault();
            }
            List<Products> productList = productService.GetAllProducts().Where(p => p.IdUser == -1).ToList();
            List<Products> CartItems = new List<Products>();

            foreach (Products p in productList)
            {
                string num = _httpContextAccessor.HttpContext.Request.Cookies[p.Id.ToString()];
                if (num != null)
                {
                    p.Number = int.Parse(num);
                    CartItems.Add(p);
                }
            }
            indexViewModel.CartItems = CartItems;

            return View(indexViewModel);
        }

        public IActionResult Checkout()
        {
            List<Products> productList = productService.GetAllProducts().Where(p => p.IdUser == -1).ToList();
            List<Products> CartItems = new List<Products>();
            int sale = 0;
            foreach (Products p in productList)
            {
                string num = _httpContextAccessor.HttpContext.Request.Cookies[p.Id.ToString()];
                if (num != null)
                {
                    p.Number = p.Number - int.Parse(num);
                    productService.UpdateNumber(p);
                    sale = sale + int.Parse(num);
                }
            }
            productService.AddSale(sale);

            foreach (var cookie in _httpContextAccessor.HttpContext.Request.Cookies.Keys)
            {

                _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Index");
        }

        public IActionResult AdminPanel()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            var temp = _httpContextAccessor.HttpContext.Request.Cookies["UserCookie"];
            if (temp != null)
            {
                indexViewModel.user = userService.GetAllUsers().Where(user => user.Id == int.Parse(temp)).FirstOrDefault();
            }
            indexViewModel.LastSales = productService.GetLastSales();
            if (indexViewModel.user.AdminIndex >= 0)
            {
                return View(indexViewModel);
            }
            return View("Index", indexViewModel);
        }

        public IActionResult ProductAdministration()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            var temp = _httpContextAccessor.HttpContext.Request.Cookies["UserCookie"];
            if (temp != null)
            {
                indexViewModel.user = userService.GetAllUsers().Where(user => user.Id == int.Parse(temp)).FirstOrDefault();
            }
            indexViewModel.products = productService.GetAllProducts();
            return View(indexViewModel);
        }

        public IActionResult EditProduct(string id)
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            var temp = _httpContextAccessor.HttpContext.Request.Cookies["UserCookie"];
            if (temp != null)
            {
                indexViewModel.user = userService.GetAllUsers().Where(user => user.Id == int.Parse(temp)).FirstOrDefault();
            }
            Products product = productService.GetAllProducts().Where(p => p.Id == int.Parse(id)).FirstOrDefault();
            return View(indexViewModel);
        }

        public IActionResult EditProductValid(string Id,string Name, string Composition,string Cities, string Price, string Number,string Picture)
        {
            Products p = productService.GetAllProducts().Where(prod => prod.Id == int.Parse(Id)).FirstOrDefault();
            p.Cities = Cities;
            p.Composition = Composition;
            p.Name = Name;
            p.Picture = Picture;
            p.Price = int.Parse(Price);
            p.Number = int.Parse(Number);
            productService.UpdateProduct(p);
            return RedirectToAction("ProductAdministration");
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        public IActionResult DeleteProduct(string id)
        {

            return RedirectToAction("ProductAdministration");
        }

        public IActionResult UserAdministration()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            var temp = _httpContextAccessor.HttpContext.Request.Cookies["UserCookie"];
            if (temp != null)
            {
                indexViewModel.user = userService.GetAllUsers().Where(user => user.Id == int.Parse(temp)).FirstOrDefault();
            }
            indexViewModel.users = userService.GetAllUsers();
            return View(indexViewModel);
        }
    }
}
