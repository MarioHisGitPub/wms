using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyConsoleApp.models;
using MyConsoleApp.data;

namespace MyConsoleApp.Services
{
    public class SupplierInvoiceService
    {
        public List<SupplierInvoice> GetAllInvoices()
        {
            using var db = new WmsDbContext();
            return db.SupplierInvoices
                .Include(si => si.Supplier)
                .Include(si => si.Receiving)
                .Include(si => si.InvoiceLines)
                    .ThenInclude(sil => sil.Product)
                .Include(si => si.InvoiceLines)
                    .ThenInclude(sil => sil.Batch)
                .ToList();
        }

        public SupplierInvoice? GetInvoiceById(int id)
        {
            using var db = new WmsDbContext();
            return db.SupplierInvoices
                .Include(si => si.Supplier)
                .Include(si => si.Receiving)
                .Include(si => si.InvoiceLines)
                    .ThenInclude(sil => sil.Product)
                .Include(si => si.InvoiceLines)
                    .ThenInclude(sil => sil.Batch)
                .FirstOrDefault(si => si.InvoiceId == id);
        }

        public void AddInvoice(SupplierInvoice invoice)
        {
            using var db = new WmsDbContext();
            invoice.CreatedDate = DateTime.Now;
            
            // Calculate totals if not already set
            if (invoice.InvoiceLines != null && invoice.InvoiceLines.Any())
            {
                invoice.SubTotal = invoice.InvoiceLines.Sum(il => il.LineTotal);
                invoice.TotalAmount = invoice.SubTotal + invoice.TaxAmount - invoice.DiscountAmount;
            }
            
            db.SupplierInvoices.Add(invoice);
            db.SaveChanges();
        }

        public void UpdateInvoice(SupplierInvoice invoice)
        {
            using var db = new WmsDbContext();
            var existing = db.SupplierInvoices.Find(invoice.InvoiceId);
            if (existing == null) return;

            existing.InvoiceNumber = invoice.InvoiceNumber;
            existing.SupplierId = invoice.SupplierId;
            existing.ReceivingId = invoice.ReceivingId;
            existing.InvoiceDate = invoice.InvoiceDate;
            existing.DueDate = invoice.DueDate;
            existing.SubTotal = invoice.SubTotal;
            existing.TaxAmount = invoice.TaxAmount;
            existing.DiscountAmount = invoice.DiscountAmount;
            existing.TotalAmount = invoice.TotalAmount;
            existing.Status = invoice.Status;
            existing.Notes = invoice.Notes;
            existing.ERPReferenceNumber = invoice.ERPReferenceNumber;
            existing.PaymentReference = invoice.PaymentReference;
            existing.ModifiedDate = DateTime.Now;
            existing.ModifiedBy = invoice.ModifiedBy;

            db.SaveChanges();
        }

        public void DeleteInvoice(int id)
        {
            using var db = new WmsDbContext();
            var invoice = db.SupplierInvoices.Find(id);
            if (invoice != null)
            {
                db.SupplierInvoices.Remove(invoice);
                db.SaveChanges();
            }
        }

        public void UpdateInvoiceStatus(int invoiceId, SupplierInvoiceStatus newStatus)
        {
            using var db = new WmsDbContext();
            var invoice = db.SupplierInvoices.Find(invoiceId);
            if (invoice != null)
            {
                invoice.Status = newStatus;
                invoice.ModifiedDate = DateTime.Now;
                
                if (newStatus == SupplierInvoiceStatus.Paid)
                {
                    invoice.PaidDate = DateTime.Now;
                }
                else if (newStatus == SupplierInvoiceStatus.SentToERP)
                {
                    invoice.SentToERPDate = DateTime.Now;
                }
                
                db.SaveChanges();
            }
        }

        public void RecordPayment(int invoiceId, string paymentReference)
        {
            using var db = new WmsDbContext();
            var invoice = db.SupplierInvoices.Find(invoiceId);
            if (invoice != null)
            {
                invoice.Status = SupplierInvoiceStatus.Paid;
                invoice.PaidDate = DateTime.Now;
                invoice.PaymentReference = paymentReference;
                invoice.ModifiedDate = DateTime.Now;
                
                db.SaveChanges();
            }
        }

        public List<SupplierInvoice> GetInvoicesBySupplier(int supplierId)
        {
            using var db = new WmsDbContext();
            return db.SupplierInvoices
                .Include(si => si.Supplier)
                .Include(si => si.Receiving)
                .Include(si => si.InvoiceLines)
                    .ThenInclude(sil => sil.Product)
                .Where(si => si.SupplierId == supplierId)
                .ToList();
        }

        public List<SupplierInvoice> GetInvoicesByStatus(SupplierInvoiceStatus status)
        {
            using var db = new WmsDbContext();
            return db.SupplierInvoices
                .Include(si => si.Supplier)
                .Include(si => si.Receiving)
                .Include(si => si.InvoiceLines)
                    .ThenInclude(sil => sil.Product)
                .Where(si => si.Status == status)
                .ToList();
        }

        public List<SupplierInvoice> GetOverdueInvoices()
        {
            using var db = new WmsDbContext();
            var today = DateTime.Now.Date;
            return db.SupplierInvoices
                .Include(si => si.Supplier)
                .Where(si => si.DueDate.HasValue && 
                            si.DueDate.Value.Date < today && 
                            si.Status != SupplierInvoiceStatus.Paid &&
                            si.Status != SupplierInvoiceStatus.Cancelled)
                .ToList();
        }

        public decimal GetTotalOutstandingForSupplier(int supplierId)
        {
            using var db = new WmsDbContext();
            return db.SupplierInvoices
                .Where(si => si.SupplierId == supplierId && 
                            (si.Status == SupplierInvoiceStatus.Draft || 
                             si.Status == SupplierInvoiceStatus.Approved ||
                             si.Status == SupplierInvoiceStatus.SentToERP))
                .Sum(si => si.TotalAmount);
        }

        public List<SupplierInvoice> GetInvoicesByReceiving(int receivingId)
        {
            using var db = new WmsDbContext();
            return db.SupplierInvoices
                .Include(si => si.Supplier)
                .Include(si => si.Receiving)
                .Include(si => si.InvoiceLines)
                    .ThenInclude(sil => sil.Product)
                .Where(si => si.ReceivingId == receivingId)
                .ToList();
        }

        public List<SupplierInvoice> GetUnpaidInvoices()
        {
            using var db = new WmsDbContext();
            return db.SupplierInvoices
                .Include(si => si.Supplier)
                .Where(si => si.Status != SupplierInvoiceStatus.Paid &&
                            si.Status != SupplierInvoiceStatus.Cancelled)
                .OrderBy(si => si.DueDate)
                .ToList();
        }
    }
}
