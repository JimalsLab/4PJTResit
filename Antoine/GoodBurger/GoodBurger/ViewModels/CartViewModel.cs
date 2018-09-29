using GoodBurger.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBurger.ViewModels
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            CartItems = new List<Products>();
            user = new Users();
        }

        public List<Products> CartItems { get; set; }
        public Users user { get; set; }
    }
}
