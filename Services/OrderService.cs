using System.Collections.Generic;
using System.Linq;
using MyConsoleApp.data;
using MyConsoleApp.models;
using Microsoft.EntityFrameworkCore;

namespace MyConsoleApp.Services
{
    public class OrderService
    {
        private readonly WmsDbContext _db;

        public OrderService()
        {
            _db = new WmsDbContext();
            _db.Database.EnsureCreated();
        }

        public List<Order> GetAllOrders()
        {
            return _db.Orders
                .Include(o => o.OrderLines)
                    .ThenInclude(ol => ol.Product)
                .ToList();
        }

        public void AddOrder(Order order)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
        }
    }
}
