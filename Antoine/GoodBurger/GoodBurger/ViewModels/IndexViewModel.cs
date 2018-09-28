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
        }
        public Users user { get; set; }

    }
}
