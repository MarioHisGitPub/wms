using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyConsoleApp.models
{
    public class SupplierInvoiceLine
    {
        [Key]
        public int InvoiceLineId { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        public SupplierInvoice Invoice { get; set; } = null!;

        [Required]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        public int? BatchId { get; set; }

        [ForeignKey(nameof(BatchId))]
        public Batch? Batch { get; set; }

        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,3)")]
        public decimal Quantity { get; set; }

        [Required]
        [StringLength(20)]
        public string Unit { get; set; } = "EA";

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxPercent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal LineTotal { get; set; }

        [StringLength(200)]
        public string? Notes { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}