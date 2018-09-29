using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodBurger.DAL
{
    public class ProductService
    {
        private Context db;
        public ProductService(Context context)
        {
            db = context;
        }

        public List<Products> GetAllProducts()
        {
            return db.products.ToList();
        }

        public void UpdateNumber(Products p)
        {
            Products temp = db.products.Where(prod => prod.Id == p.Id).FirstOrDefault();
            temp.Number = p.Number;
            db.SaveChanges();
        }
        public void UpdateProduct(Products p)
        {
            Products temp = db.products.Where(prod => prod.Id == p.Id).FirstOrDefault();
            temp.Number = p.Number;
            temp.Cities = p.Cities;
            temp.Composition = p.Composition;
            temp.Name = p.Name;
            temp.Picture = p.Picture;
            temp.Price = p.Price;
            db.SaveChanges();
        }

        public List<Sales> GetLastSales()
        {
            List<Sales> sales = db.sales.ToList();
            sales.Reverse();
            return sales.Take(8).ToList();
        }
        public void AddSale(int price)
        {
            Sales sale = new Sales
            {
                Price = price,
                Date = DateTime.Now
            };
            db.sales.Add(sale);
            db.SaveChanges();
        }
        public void DeleteProduct(string id)
        {
            db.products.Remove(db.products.Where(p => p.Id == int.Parse(id)).FirstOrDefault());
        }
    }
}
