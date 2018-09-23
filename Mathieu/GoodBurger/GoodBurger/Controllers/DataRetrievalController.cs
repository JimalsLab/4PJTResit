using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EvoPdf.HtmlToPdfClient;
using GoodBurger.Context;
using GoodBurger.Services;
using GoodBurger.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoodBurger.Controllers
{
    [Route("DataRetrieval")]
    public class DataRetrievalController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private DataRetrievalService service;

        public DataRetrievalController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            service = new DataRetrievalService();
        }

        // GET: api/DataRetrieval
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //exemple
            return new string[] { "a", "b" };
        }

        [HttpGet("[action]")]
        public IEnumerable<string> GetNames()
        {
            var result = service.GetUsers().Where(x => x.Id >= 1).ToList();
            return new string[] { result[0].Name, result[1].Name };
        }

        [HttpGet("[action]")]
        public List<Burgers> GetProducts()
        {
            List<Burgers> result = service.GetProducts().Where(x => x.Type != "Menu" && x.IdCart == -1).ToList();
            return result;
        }

        [HttpGet("[action]")]
        public Users GetCurrentUser()
        {
            string id = _httpContextAccessor.HttpContext.Request.Cookies["userCookie"];
            if (id == null || id == "")
            {
                return null;
            }
            else
            {
                Users result = service.GetUsers().Where(x => x.Id == int.Parse(id)).FirstOrDefault();
                return result;
            }
            
        }

        public AdminPanelViewModel GetAdminPanelInfo()
        {
            return null;////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        [HttpGet("[action]")]
        public UserToken GetUserToken()
        {
            UserToken ut = new UserToken
            {
                id = -1,
                name = "",
                isadmin = -1
            };
            string id = _httpContextAccessor.HttpContext.Request.Cookies["userCookie"];
            if (id == null || id == "")
            {
                return ut;
            }
            else
            {
                Users u = service.GetUsers().Where(x => x.Id == int.Parse(id)).FirstOrDefault();
                if (u.Name == null || u.Name.Length < 2)
                {
                    ut.name = u.Username;
                }
                else
                {
                    ut.name = u.Name;
                }
                ut.id = u.Id;
                ut.isadmin = u.IsAdmin;

                 return ut;
            }
        }

        [HttpGet("[action]")]
        public IActionResult Items(string stock, string itemid, string productnb)
        {
            if (productnb == null) { productnb = "1"; }
            Burgers burger = service.GetProducts().Where(x => x.Id == int.Parse(itemid)).FirstOrDefault();
            CartValidViewModel cvvm = new CartValidViewModel
            {
                Cities = burger.Cities,
                Components = burger.Components,
                Description = burger.Description,
                HowMany = int.Parse(productnb),
                Id = burger.Id,
                IdCart = burger.IdCart,
                Name = burger.Name,
                Number = burger.Number,
                Picture = burger.Picture,
                Price = burger.Price,
                Type = burger.Type,
                Stock = int.Parse(stock)
            };

            if (int.Parse(productnb) > int.Parse(stock))
            {
                cvvm.message = "Oops, we are short on stock for this object !";
                cvvm.Colorstate = "red";
            }
            else
            {

                Burgers b = new Burgers
                {
                    Name = burger.Name,
                    Price = burger.Price,
                    Children = burger.Children,
                    Components = burger.Components,
                    
                    Number = int.Parse(productnb),
                    Picture = burger.Picture,
                    Cities = burger.Cities,
                    Description = burger.Description,
                    Type = burger.Type       
                };
                string temp = _httpContextAccessor.HttpContext.Request.Cookies["sessionCookie"];
                if (temp == null || temp == "")
                {

                    CookieOptions session = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1)
                    };
                    Carts c = service.GetCartByUserId(GetCurrentUser().Id);
                    _httpContextAccessor.HttpContext.Response.Cookies.Append("sessionCookie",c.Guid, session);
                    b.IdCart = c.Id;
                }
                else
                {
                    b.IdCart = service.GetCartIdByGuid(temp);
                }

                bool error = service.AddProductToBasket(b);
                
                //+ add to cart (or create new cart)
                //+save to db +cart saved to db if new cart
                if (error)
                {
                    cvvm.message = "Oops, something broke, try again later !";
                    cvvm.Colorstate = "red";
                }
                if (cvvm.HowMany == 1)
                {
                    cvvm.message = "Item added to cart";
                    cvvm.Colorstate = "green";
                }
                else if (cvvm.HowMany == 0)
                {
                    cvvm.message = "No items added to cart";
                }
                else
                {
                    cvvm.message = "Items added to cart";
                    cvvm.Colorstate = "green";
                }


            }

            return View("CartInfo",cvvm);
        }

        [HttpGet("[action]")]
        public List<Burgers> CartItems()
        {
            int idCart = service.GetCartIdByGuid(_httpContextAccessor.HttpContext.Request.Cookies["sessionCookie"]);
            var result = service.GetItemsInCart(idCart);
            return result;
        }

        [HttpGet("/Item/{id}")]
        public Burgers Item(string id)
        {
            var result = service.GetProducts().Where(x => x.Id == int.Parse(id)).FirstOrDefault();
            return result;
        }

        // GET: api/DataRetrieval/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/DataRetrieval
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/DataRetrieval/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
        [HttpGet("[action]")]
        public ActionResult DeleteCartItem(string id)
        {
            Burgers burger = service.GetProducts().Where(x => x.Id == int.Parse(id)).FirstOrDefault();
            
            CartValidViewModel cvvm = new CartValidViewModel
            {
                Cities = burger.Cities,
                Components = burger.Components,
                Description = burger.Description,
                Id = burger.Id,
                IdCart = burger.IdCart,
                Name = burger.Name,
                Number = burger.Number,
                Picture = burger.Picture,
                Price = burger.Price,
                Type = burger.Type,
            };
            bool error = service.DeleteBurger(burger);
            if (!error)
            {
                cvvm.message = "Oops, something broke, try again later !";
                cvvm.Colorstate = "red";
            }
            else
            {
                cvvm.message = cvvm.Name + " deleted successfully from cart";
                cvvm.Colorstate = "green";
            }

            return View("CartInfo", cvvm);
        }

        [HttpGet("[action]")]
        public ActionResult Checkout()
        {
            Users u = service.GetUsers().Where(x => x.Id == int.Parse(_httpContextAccessor.HttpContext.Request.Cookies["userCookie"])).FirstOrDefault();
            Carts c = service.GetCartByUserId(u.Id);
            List<Burgers> cartItems = service.GetItemsInCart(c.Id);

            CheckoutViewModel cvm = new CheckoutViewModel();

            try
            {
                foreach (Burgers b in cartItems)
                {
                    Burgers original = service.GetProducts().Where(x => x.IdCart == -1 && x.Name == b.Name).FirstOrDefault();
                    original.Number = original.Number - b.Number;
                    service.UpdateBurgerInfo(original);
                    service.DeleteBurger(b);
                }

                cvm.message = "Thanks for buying at GoodBurger !";
                cvm.Colorstate = "green";
                cvm.cartItems = cartItems;
                
            }
            catch
            {
                cvm.message = "Oops ! Something went wrong, try later";
                cvm.Colorstate = "red";
            }
            return View(cvm);
        }

        [HttpGet("[action]")]
        public ActionResult PdfView(string cartid)
        {
            return View();
        }

        [HttpGet("[action]")]
        public ActionResult UpdateOrRegister(string username, string password, string passwordrepeat, string address)
        {
            SessionInfoViewModel sivm = new SessionInfoViewModel();
            if (password != passwordrepeat)
            {
                sivm.message = "Passwords don't match ";
            }
            else if (service.GetUsers().Where(x => x.Username == username).FirstOrDefault() != default(Users))
            {
                sivm.message = "Username already taken ";
            }
            else
            {
                Users u = new Users
                {
                    Address = address,
                    IsAdmin = -1,
                    Name = username,
                    Password = password,
                    Username = username
                };
                bool error = false;
                int newid = -1;
                string id = _httpContextAccessor.HttpContext.Request.Cookies["userCookie"];
                if (id == null || id == "")
                {
                   newid  = service.AddUser(u);

                    CookieOptions session = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1)
                    };

                    _httpContextAccessor.HttpContext.Response.Cookies.Append("userCookie", newid.ToString(), session);

                    sivm.message = "Account created successfully ! Welcome " + username;
                    error = true;
                }
                else
                {
                    u.Id = int.Parse(id);
                    error = service.UpdateInfoUser(u);
                    sivm.message = "Account updated successfully ";
                    error = true;
                }


                if (!error)
                {
                    sivm.message = "Oops, couldn't retrieve your account, pleanse try again later !";
                }

                
            }
            return View("UserInfo",sivm);
        }

        [HttpGet("[action]")]
        public ActionResult Login(string username, string password)
        {
            DisconnectionInfoViewModel divm = new DisconnectionInfoViewModel();
            Users u = service.GetUsers().Where(x => x.Username == username).FirstOrDefault();
            if (u == null)
            {
                divm.message = "Wrong Username";
                divm.Colorstate = "red";
            }
            else
            {
                if (password == u.Password)
                {
                    divm.message = "Successfully connected";
                    divm.Colorstate = "green";

                    CookieOptions session = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1)
                    };

                    _httpContextAccessor.HttpContext.Response.Cookies.Append("userCookie", u.Id.ToString(), session);

                }
                else
                {
                    divm.message = "Wrong Password";
                    divm.Colorstate = "red";
                }
            }

            return View("DisconnectionInfo", divm);
        }

        [HttpGet("[action]")]
        public ActionResult Disconnect()
        {
            DisconnectionInfoViewModel divm = new DisconnectionInfoViewModel();
            try
            {
                foreach (var cookie in _httpContextAccessor.HttpContext.Request.Cookies.Keys)
                {
                    _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie);
                }
                divm.message = "You were succesfully disconnected";
                divm.Colorstate = "green";
            }
            catch (Exception e)
            {
                divm.message = "Oops, couldn't disconnect you ! please try again later";
                divm.Colorstate = "red";
            }

            return View("DisconnectionInfo",divm);
        }

        public class UserToken
        {
            public string name { get; set; }
            public int id { get; set; }
            public int isadmin { get; set; }
        }
    }
}
