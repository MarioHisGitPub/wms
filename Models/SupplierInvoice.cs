using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyConsoleApp.models
{
    public class SupplierInvoice
    {
        [Key]
        public int InvoiceId { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; } = string.Empty;

        [Required]
        public int SupplierId { get; set; }

        [ForeignKey(nameof(SupplierId))]
        public Supplier Supplier { get; set; } = null!;

        public int? ReceivingId { get; set; }

        [ForeignKey(nameof(ReceivingId))]
        public Receiving? Receiving { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        public DateTime? DueDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        public SupplierInvoiceStatus Status { get; set; } = SupplierInvoiceStatus.Draft;

        [StringLength(500)]
        public string? Notes { get; set; }

        [StringLength(100)]
        public string? ERPReferenceNumber { get; set; }

        public DateTime? SentToERPDate { get; set; }

        public DateTime? PaidDate { get; set; }

        [StringLength(50)]
        public string? PaymentReference { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string? CreatedBy { get; set; }

        [StringLength(100)]
        public string? ModifiedBy { get; set; }

        // Navigation properties
        public ICollection<SupplierInvoiceLine> InvoiceLines { get; set; } = new List<SupplierInvoiceLine>();
    }
}