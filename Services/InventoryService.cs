using System.Collections.Generic;
using System.Linq;
using MyConsoleApp.data;
using MyConsoleApp.models;
using Microsoft.EntityFrameworkCore;

namespace MyConsoleApp.Services
{
    public class InventoryService
    {
        private readonly WmsDbContext _db;

        public InventoryService()
        {
            _db = new WmsDbContext();
            _db.Database.EnsureCreated();
        }

        public List<Inventory> GetAllInventory()
        {
            return _db.Inventories
                .Include(i => i.Product)
                .Include(i => i.Location)
                .ToList();
        }

        public void AddInventory(Inventory item)
        {
            _db.Inventories.Add(item);
            _db.SaveChanges();
        }
    }
}
