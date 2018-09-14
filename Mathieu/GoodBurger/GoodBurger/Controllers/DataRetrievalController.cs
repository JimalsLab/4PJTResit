using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        private DataRetrievalService service;

        public DataRetrievalController()
        {
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
            int idCart = service.GetCartIdByGuid(Request.Cookies["sessionCookie"]);
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
    }
}
