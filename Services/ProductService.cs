using System.Collections.Generic;
using System.Linq;
using MyConsoleApp.data;
using MyConsoleApp.models;

namespace MyConsoleApp.Services
{
    public class ProductService
    {
        private readonly WmsDbContext _db;

        public ProductService()
        {
            _db = new WmsDbContext();
            _db.Database.EnsureCreated();
        }

        public List<Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        public void AddProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
        }
    }
}
