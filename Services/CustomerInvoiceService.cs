using System;
using System.Collections.Generic;
using MyConsoleApp.models;

namespace MyConsoleApp.Services
{
    public class CustomerInvoiceService
    {
        private static readonly List<CustomerInvoice> _fakeInvoices = new()
        {
            new CustomerInvoice
            {
                InvoiceId = 1,
                InvoiceNumber = "INV-2026-001",
                Customer = new Customer
                {
                    Id = 1,
                    Name = "Acme Corp",
                    Email = "info@acme.com",
                    Phone = "555-1234"
                },
                OrderId = 1,
                InvoiceDate = DateTime.Now.AddDays(-7),
                SubTotal = 1500m,
                TaxAmount = 150m,
                ShippingAmount = 50m,
                DiscountAmount = 0m,
                TotalAmount = 1700m,
                PaidAmount = 0m,
                BalanceAmount = 1700m,
                Status = CustomerInvoiceStatus.Draft,
                CreatedDate = DateTime.Now
            }
        };

        public List<CustomerInvoice> GetAllInvoices()
        {
            return _fakeInvoices;
        }

        public void AddInvoice(CustomerInvoice invoice)
        {
            invoice.InvoiceId = _fakeInvoices.Count + 1;
            _fakeInvoices.Add(invoice);
        }
    }
}
