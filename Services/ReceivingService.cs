using System.Collections.Generic;
using System.Linq;
using MyConsoleApp.data;
using MyConsoleApp.models;
using Microsoft.EntityFrameworkCore;

namespace MyConsoleApp.Services
{
    public class ReceivingService
    {
        private readonly WmsDbContext _db;

        public ReceivingService()
        {
            _db = new WmsDbContext();
            _db.Database.EnsureCreated();
        }

        public List<Receiving> GetAllReceivings()
        {
            return _db.Receivings
                .Include(r => r.Supplier)
                .Include(r => r.Product)
                .Include(r => r.Location)
                .ToList();
        }

        public void AddReceiving(Receiving receiving)
        {
            _db.Receivings.Add(receiving);
            
            // Update inventory - increase stock when received
            if (receiving.Status == "Received" && receiving.LocationId.HasValue)
            {
                var inventory = _db.Inventories
                    .FirstOrDefault(i => i.ProductId == receiving.ProductId 
                                      && i.LocationId == receiving.LocationId.Value);
                
                if (inventory != null)
                {
                    inventory.Quantity += receiving.Quantity;
                }
                else
                {
                    _db.Inventories.Add(new Inventory
                    {
                        ProductId = receiving.ProductId,
                        LocationId = receiving.LocationId.Value,
                        Quantity = receiving.Quantity
                    });
                }
            }
            
            _db.SaveChanges();
        }
    }
}
