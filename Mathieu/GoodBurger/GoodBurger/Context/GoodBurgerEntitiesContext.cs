
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBurger.Context
{


    public partial class GoodBurgerEntitiesContext : DbContext
    {


        public DbSet<User> Users { get; set; }
        public DbSet<Burger> Burgers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Menu> Menus { get; set; }

    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public int IsAdmin { get; set; }
    }

    public class Burger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Picture { get; set; }
        public string Number { get; set; }
        public int IdCart { get; set; }
    }

    public class Cart
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
    }

    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
    }
}
