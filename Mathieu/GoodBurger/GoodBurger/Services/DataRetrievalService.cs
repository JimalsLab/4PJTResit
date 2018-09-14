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
        #region get
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
                return db.Burger.ToList();
            }
        }

        public int GetCartIdByGuid(string guid)
        {
            using (var db = new GoodBurgerEntitiesContext())
            {
                return db.Cart.Where(cart => cart.Guid == guid).FirstOrDefault().Id;
            }
        }

        public List<Burgers> GetItemsInCart(int id)
        {
            using (var db = new GoodBurgerEntitiesContext())
            {
                return db.Burger.Where(burger => burger.IdCart == id).ToList();
            }
        }

        #endregion

        #region post
        public bool AddProductToBasket(Burgers b)
        {
            using (var db = new GoodBurgerEntitiesContext())
            {
                try
                {
                    db.Burger.Add(b);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool CreateCart(Carts c)
        {
            using (var db = new GoodBurgerEntitiesContext())
            {
                try
                {
                    db.Cart.Add(c);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        #endregion
    }
}
