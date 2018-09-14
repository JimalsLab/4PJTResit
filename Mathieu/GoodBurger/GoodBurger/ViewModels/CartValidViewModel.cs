using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBurger.ViewModels
{
    public class CartValidViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string Picture { get; set; }
        public int? Number { get; set; }
        public int? IdCart { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Components { get; set; }
        public string Cities { get; set; }
        public int HowMany { get; set; }
        public string message { get; set; }
        public int Stock { get; set; }
        public string Colorstate { get; set; }
    }
}
