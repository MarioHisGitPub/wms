using System.Collections.Generic;
using System.Linq;
using MyConsoleApp.data;
using MyConsoleApp.models;
using Microsoft.EntityFrameworkCore;

namespace MyConsoleApp.Services
{
    public class DispatchService
    {
        private readonly WmsDbContext _db;

        public DispatchService()
        {
            _db = new WmsDbContext();
            _db.Database.EnsureCreated();
        }

        public List<Dispatch> GetAllDispatches()
        {
            return _db.Dispatches
                .Include(d => d.Order)
                .Include(d => d.Product)
                .ToList();
        }

        public void AddDispatch(Dispatch dispatch)
        {
            // Dispatch doesn't change inventory - picking already did that
            _db.Dispatches.Add(dispatch);
            _db.SaveChanges();
        }
    }
}
