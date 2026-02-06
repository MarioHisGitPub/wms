using System.Collections.Generic;
using System.Linq;
using MyConsoleApp.data;
using MyConsoleApp.models;

namespace MyConsoleApp.Services
{
    public class ReportService
    {
        private readonly WmsDbContext _db;

        public ReportService()
        {
            _db = new WmsDbContext();
            _db.Database.EnsureCreated();
        }

        public List<Report> GetAllReports() => _db.Reports.ToList();

        public void AddReport(Report report)
        {
            _db.Reports.Add(report);
            _db.SaveChanges();
        }
    }
}
