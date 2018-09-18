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

        public int AddUser(Users u)
        {
            using (var db = new GoodBurgerEntitiesContext())
            {
                try
                {
                    db.User.Add(u);
                    db.SaveChanges();
                    return db.User.Where(x=>x.Username == u.Username).FirstOrDefault().Id;
                }
                catch
                {
                    return -1;
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

        #region delete

        public bool DeleteBurger(Burgers b)
        {
            using (var db = new GoodBurgerEntitiesContext())
            {
                try
                {
                    db.Burger.Remove(b);
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

        #region update

        public bool UpdateInfoUser(Users user)
        {
            using (var db = new GoodBurgerEntitiesContext())
            {
                try
                {
                    Users u = db.User.Where(x => x.Id == user.Id).FirstOrDefault();
                    u.Address = user.Address;
                    u.Password = user.Password;
                    u.Username = user.Username;
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
