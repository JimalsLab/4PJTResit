using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBurger.DAL
{
    public class Context :  DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GoodBurgerDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public DbSet<Users> users { get; set; }
        public DbSet<Products> products { get; set; }

        
    }

    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public int AdminIndex { get; set; }
    }

    public class Products
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string Picture { get; set; }
        public int? Number { get; set; }
        public int? IdUser { get; set; }
        public string Composition { get; set; }
        public string Cities { get; set; }

    }
}
