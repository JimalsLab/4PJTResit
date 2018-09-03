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
    [Route("api/DataRetrieval")]
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
            List<Burgers> result = service.GetProducts().Where(x => x.Type != "Menu" && x.OnCart == 0).ToList();
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
