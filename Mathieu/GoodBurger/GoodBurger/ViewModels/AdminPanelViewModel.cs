using GoodBurger.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBurger.ViewModels
{
    public class AdminPanelViewModel
    {
        public AdminPanelViewModel()
        {
            MenuBurgers = new List<Burgers>();
            BoughtBurgers = new List<Burgers>();
        }
        public List<Burgers> MenuBurgers { get; set; }
        public List<Burgers> BoughtBurgers { get; set; }
        public int Sales { get; set; }
    }
}
