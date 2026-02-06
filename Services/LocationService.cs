using System.Collections.Generic;
using System.Linq;
using MyConsoleApp.data;
using MyConsoleApp.models;

namespace MyConsoleApp.Services
{
    public class LocationService
    {
        private readonly WmsDbContext _db;

        public LocationService()
        {
            _db = new WmsDbContext();
            _db.Database.EnsureCreated();
        }

        public List<Location> GetAllLocations() => _db.Locations.ToList();

        public void AddLocation(Location location)
        {
            _db.Locations.Add(location);
            _db.SaveChanges();
        }
    }
}
