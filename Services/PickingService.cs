using System.Collections.Generic;
using System.Linq;
using MyConsoleApp.data;
using MyConsoleApp.models;
using Microsoft.EntityFrameworkCore;

namespace MyConsoleApp.Services
{
    public class PickingService
    {
        private readonly WmsDbContext _db;

        public PickingService()
        {
            _db = new WmsDbContext();
            _db.Database.EnsureCreated();
        }

        public List<Picking> GetAllPickings()
        {
            return _db.Pickings
                .Include(p => p.Order)
                .Include(p => p.Product)
                .Include(p => p.Location)
                .ToList();
        }

        public void AddPicking(Picking picking)
        {
            _db.Pickings.Add(picking);
            
            // ALWAYS reduce inventory when picking (even if status is Pending)
            // Because picking means removing from warehouse location
            var inventory = _db.Inventories
                .FirstOrDefault(i => i.ProductId == picking.ProductId 
                                  && i.LocationId == picking.LocationId);
            
            if (inventory != null && inventory.Quantity >= picking.Quantity)
            {
                inventory.Quantity -= picking.Quantity;
            }
            
            _db.SaveChanges();
        }
    }
}
