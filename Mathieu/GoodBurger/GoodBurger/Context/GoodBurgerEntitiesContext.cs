
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBurger.Context
{
    

    public partial class GoodBurgerEntitiesContext : DbContext
    {
        string password = "a";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:goodburger.database.windows.net,1433;Initial Catalog=GoodBurgerDB;Persist Security Info=False;User ID=supinfo;Password="+password+";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<Users> User { get; set; }
        public DbSet<Burgers> Burger { get; set; }
        public DbSet<Carts> Cart { get; set; }
        public DbSet<Menus> Menu { get; set; }

    }

    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public int IsAdmin { get; set; }
    }

    public class Burgers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Picture { get; set; }
        public int Number { get; set; }
        public int IdCart { get; set; }
        public string Type { get; set; }
        public string Children { get; set; }
        public string Description { get; set; }
        public string Components { get; set; }
        public int OnCart { get; set; }

    }

    public class Carts
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
    }

    public class Menus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }

        public string Type { get; set; }
    }
}
