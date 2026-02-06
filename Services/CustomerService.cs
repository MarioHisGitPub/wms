using System;
using System.Collections.Generic;
using MyConsoleApp.models;  // matches your models namespace

namespace MyConsoleApp.Services
{
    public class CustomerService
    {
        // In-memory fake data
        private static readonly List<Customer> _fakeCustomers = new()
        {
            new Customer { Id = 1, Name = "Acme Corp", Email = "info@acme.com", Phone = "555-1234" },
            new Customer { Id = 2, Name = "John Doe", Email = "john@example.com", Phone = "555-5678" }
        };

        public List<Customer> GetAllCustomers()
        {
            return _fakeCustomers;
        }

        public void AddCustomer(Customer customer)
        {
            customer.Id = _fakeCustomers.Count + 1;
            _fakeCustomers.Add(customer);
        }
    }
}
