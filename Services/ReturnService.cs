using System.Collections.Generic;
using System.Linq;
using MyConsoleApp.data;
using MyConsoleApp.models;
using Microsoft.EntityFrameworkCore;

namespace MyConsoleApp.Services
{
    public class ReturnService
    {
        private readonly WmsDbContext _db;

        public ReturnService()
        {
            _db = new WmsDbContext();
            _db.Database.EnsureCreated();
        }

        public List<Return> GetAllReturns()
        {
            return _db.Returns
                .Include(r => r.Order)
                .Include(r => r.Product)
                .Include(r => r.Location)
                .ToList();
        }

        public void AddReturn(Return returnItem)
        {
            _db.Returns.Add(returnItem);
            
            // Update inventory - increase stock when return is processed
            if (returnItem.Status == "Processed" && returnItem.LocationId.HasValue)
            {
                var inventory = _db.Inventories
                    .FirstOrDefault(i => i.ProductId == returnItem.ProductId 
                                      && i.LocationId == returnItem.LocationId.Value);
                
                if (inventory != null)
                {
                    inventory.Quantity += returnItem.Quantity;
                }
                else
                {
                    _db.Inventories.Add(new Inventory
                    {
                        ProductId = returnItem.ProductId,
                        LocationId = returnItem.LocationId.Value,
                        Quantity = returnItem.Quantity
                    });
                }
            }
            
            _db.SaveChanges();
        }
    }
}
