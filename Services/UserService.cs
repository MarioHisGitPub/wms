using System.Collections.Generic;
using System.Linq;
using MyConsoleApp.data;
using MyConsoleApp.models;

namespace MyConsoleApp.services
{
    public class UserService
    {
        private readonly WmsDbContext _db;

        public UserService()
        {
            _db = new WmsDbContext();
            _db.Database.EnsureCreated();
        }

        public List<User> GetAllUsers() => _db.Users.ToList();

        public void AddUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }
    }
}