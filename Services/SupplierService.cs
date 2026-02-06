using System.Collections.Generic;
using System.Linq;
using MyConsoleApp.data;
using MyConsoleApp.models;

namespace MyConsoleApp.Services
{
    public class SupplierService
    {
        private readonly WmsDbContext _db;

        public SupplierService()
        {
            _db = new WmsDbContext();
            _db.Database.EnsureCreated();
        }

        public List<Supplier> GetAllSuppliers() => _db.Suppliers.ToList();

        public void AddSupplier(Supplier supplier)
        {
            _db.Suppliers.Add(supplier);
            _db.SaveChanges();
        }
    }
}
