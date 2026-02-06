using Microsoft.EntityFrameworkCore;
using MyConsoleApp.models;

namespace MyConsoleApp.data
{
    public class WmsDbContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Inventory> Inventories => Set<Inventory>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderLine> OrderLines => Set<OrderLine>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Picking> Pickings => Set<Picking>();
        public DbSet<Dispatch> Dispatches => Set<Dispatch>();
        public DbSet<Receiving> Receivings => Set<Receiving>();
        public DbSet<AdjustStock> AdjustStocks => Set<AdjustStock>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<Return> Returns => Set<Return>();
        public DbSet<Batch> Batches => Set<Batch>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Report> Reports => Set<Report>();
        
        // Customer Invoicing - NEW
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<CustomerInvoice> CustomerInvoices => Set<CustomerInvoice>();
        public DbSet<CustomerInvoiceLine> CustomerInvoiceLines => Set<CustomerInvoiceLine>();
        public DbSet<CustomerPayment> CustomerPayments => Set<CustomerPayment>();
        public DbSet<SupplierInvoice> SupplierInvoices => Set<SupplierInvoice>();
        public DbSet<SupplierInvoiceLine> SupplierInvoiceLines => Set<SupplierInvoiceLine>();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=wms.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // OrderLine relationships
            modelBuilder.Entity<OrderLine>()
                .HasOne(ol => ol.Product)
                .WithMany()
                .HasForeignKey(ol => ol.ProductId);

            modelBuilder.Entity<OrderLine>()
                .HasOne(ol => ol.Order)
                .WithMany(o => o.OrderLines)
                .HasForeignKey(ol => ol.OrderId);

            // Inventory relationships
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId);

            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Location)
                .WithMany()
                .HasForeignKey(i => i.LocationId);

            // AdjustStock relationships
            modelBuilder.Entity<AdjustStock>()
                .HasOne(a => a.Product)
                .WithMany()
                .HasForeignKey(a => a.ProductId);

            modelBuilder.Entity<AdjustStock>()
                .HasOne(a => a.Location)
                .WithMany()
                .HasForeignKey(a => a.LocationId);

            modelBuilder.Entity<AdjustStock>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId);

            // Receiving relationships
            modelBuilder.Entity<Receiving>()
                .HasOne(r => r.Supplier)
                .WithMany()
                .HasForeignKey(r => r.SupplierId);

            modelBuilder.Entity<Receiving>()
                .HasOne(r => r.Product)
                .WithMany()
                .HasForeignKey(r => r.ProductId);

            modelBuilder.Entity<Receiving>()
                .HasOne(r => r.Location)
                .WithMany()
                .HasForeignKey(r => r.LocationId);

            // Return relationships
            modelBuilder.Entity<Return>()
                .HasOne(r => r.Order)
                .WithMany()
                .HasForeignKey(r => r.OrderId);

            modelBuilder.Entity<Return>()
                .HasOne(r => r.Product)
                .WithMany()
                .HasForeignKey(r => r.ProductId);

            modelBuilder.Entity<Return>()
                .HasOne(r => r.Location)
                .WithMany()
                .HasForeignKey(r => r.LocationId);

            // Picking relationships
            modelBuilder.Entity<Picking>()
                .HasOne(p => p.Order)
                .WithMany()
                .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<Picking>()
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<Picking>()
                .HasOne(p => p.Location)
                .WithMany()
                .HasForeignKey(p => p.LocationId);

            // Dispatch relationships
            modelBuilder.Entity<Dispatch>()
                .HasOne(d => d.Order)
                .WithMany()
                .HasForeignKey(d => d.OrderId);

            modelBuilder.Entity<Dispatch>()
                .HasOne(d => d.Product)
                .WithMany()
                .HasForeignKey(d => d.ProductId);

            // Batch relationships
            modelBuilder.Entity<Batch>()
                .HasOne(b => b.Product)
                .WithMany()
                .HasForeignKey(b => b.ProductId);

            modelBuilder.Entity<Batch>()
                .HasOne(b => b.Location)
                .WithMany()
                .HasForeignKey(b => b.LocationId);

            // Customer Invoice relationships - NEW
            modelBuilder.Entity<CustomerInvoice>()
                .HasOne(ci => ci.Customer)
                .WithMany()
                .HasForeignKey(ci => ci.CustomerId);

            modelBuilder.Entity<CustomerInvoice>()
                .HasOne(ci => ci.Order)
                .WithMany()
                .HasForeignKey(ci => ci.OrderId);

            modelBuilder.Entity<CustomerInvoice>()
                .HasOne(ci => ci.Dispatch)
                .WithMany()
                .HasForeignKey(ci => ci.DispatchId)
                .IsRequired(false);

            modelBuilder.Entity<CustomerInvoiceLine>()
                .HasOne(cil => cil.Invoice)
                .WithMany(ci => ci.InvoiceLines)
                .HasForeignKey(cil => cil.InvoiceId);

            modelBuilder.Entity<CustomerInvoiceLine>()
                .HasOne(cil => cil.Product)
                .WithMany()
                .HasForeignKey(cil => cil.ProductId);

            modelBuilder.Entity<CustomerInvoiceLine>()
                .HasOne(cil => cil.OrderLine)
                .WithMany()
                .HasForeignKey(cil => cil.OrderLineId)
                .IsRequired(false);

            modelBuilder.Entity<CustomerInvoiceLine>()
                .HasOne(cil => cil.Batch)
                .WithMany()
                .HasForeignKey(cil => cil.BatchId)
                .IsRequired(false);

            modelBuilder.Entity<CustomerPayment>()
                .HasOne(cp => cp.Invoice)
                .WithMany()
                .HasForeignKey(cp => cp.InvoiceId);

            // Supplier Invoice relationships - NEW
            modelBuilder.Entity<SupplierInvoice>()
                .HasOne(si => si.Supplier)
                .WithMany()
                .HasForeignKey(si => si.SupplierId);

            modelBuilder.Entity<SupplierInvoiceLine>()
                .HasOne(sil => sil.Invoice)
                .WithMany(si => si.InvoiceLines)
                .HasForeignKey(sil => sil.InvoiceId);

            modelBuilder.Entity<SupplierInvoiceLine>()
                .HasOne(sil => sil.Product)
                .WithMany()
                .HasForeignKey(sil => sil.ProductId);

            modelBuilder.Entity<SupplierInvoiceLine>()
                .HasOne(sil => sil.Batch)
                .WithMany()
                .HasForeignKey(sil => sil.BatchId)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}