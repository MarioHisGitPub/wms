using System.Collections.Generic;
using System.Linq;
using MyConsoleApp.data;
using MyConsoleApp.models;
using Microsoft.EntityFrameworkCore;

namespace MyConsoleApp.Services
{
    public class BatchService
    {
        private readonly WmsDbContext _db;

        public BatchService()
        {
            _db = new WmsDbContext();
            _db.Database.EnsureCreated();
        }

        public List<Batch> GetAllBatches()
        {
            return _db.Batches
                .Include(b => b.Product)
                .Include(b => b.Location)
                .ToList();
        }

        public void AddBatch(Batch batch)
        {
            _db.Batches.Add(batch);
            _db.SaveChanges();
        }
    }
}
