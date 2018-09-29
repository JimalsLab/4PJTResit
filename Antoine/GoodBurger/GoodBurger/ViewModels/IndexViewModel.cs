using GoodBurger.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBurger.ViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            user = new Users();
            product = new Products();
            CartItems = new List<Products>();
            products = new List<Products>();
            LastSales = new List<Sales>();
            users = new List<Users>();
        }
        public Users user { get; set; }
        public Products product { get; set; }
        public List<Products> products { get; set; }
        public List<Users> users { get; set; }
        public List<Products> CartItems { get; set; }
        public List<Sales> LastSales { get; set; }
    }
}
