using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBurger.DAL
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Users> users { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<Sales> sales { get; set; }

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

    public class Sales
    {
        public int? Id { get; set; }
        public int? Price { get; set; }
        public DateTime Date { get; set; }
    }
}
