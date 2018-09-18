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
                    IdCart = service.GetCartIdByGuid(Request.Cookies["sessionCookie"]),
                    Number = int.Parse(productnb),
                    Picture = burger.Picture,
                    Cities = burger.Cities,
                    Description = burger.Description,
                    Type = burger.Type       
                };

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
        public ActionResult Checkout(string id)
        {
            try
            {
                // create the HTML to PDF converter object
                HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();

                // set license key
                htmlToPdfConverter.LicenseKey = "4W9+bn19bn5ue2B+bn1/YH98YHd3d3c=";

                // Set an adddional delay in seconds to wait for JavaScript or AJAX calls after page load completed
                // Set this property to 0 if you don't need to wait for such asynchcronous operations to finish
                htmlToPdfConverter.ConversionDelay = 2;

                // set PDF page size
                htmlToPdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;

                // set PDF page orientation
                htmlToPdfConverter.PdfDocumentOptions.PdfPageOrientation = PdfPageOrientation.Portrait;

                // convert the HTML page from given URL to PDF in a buffer
                byte[] pdfBytes = htmlToPdfConverter.ConvertUrl("/DataRetrieval/PdfView");

                // write the PDF buffer in output file
                System.IO.File.WriteAllBytes("EvoHtmlToPdf.pdf", pdfBytes);

                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = "testPDF.pdf",
                    Inline = false,
                };
                return File(pdfBytes, "application/pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Error: {0}", ex.Message));
            }
            return View();
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
    }
}
