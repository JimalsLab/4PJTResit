using GoodBurger.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBurger.Services
{
    public class DataRetrievalService
    {

        public List<Users> GetUsers()
        {
            using (var db = new GoodBurgerEntitiesContext())
            {
                return db.User.ToList();
            }
        }

        public List<Burgers> GetProducts()
        {
            using (var db = new GoodBurgerEntitiesContext())
            {
                return db.Burger.Where(x=>x.Type !="Menu" && x.OnCart == 0).ToList();
            }
        }
    }
}
