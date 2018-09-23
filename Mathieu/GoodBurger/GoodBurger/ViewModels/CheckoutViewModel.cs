using GoodBurger.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBurger.ViewModels
{
    public class CheckoutViewModel
    {
        public CheckoutViewModel()
        {
            cartItems = new List<Burgers>();
        }

        public List<Burgers> cartItems { get; set; }

        public string message { get; set; }
        public string Colorstate { get; set; }
    }
}
