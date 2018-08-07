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

        public List<User> GetUsers()
        {
            using (var db = new GoodBurgerEntitiesContext())
            {
                return db.Users.ToList();
            }
        }
    }
}
