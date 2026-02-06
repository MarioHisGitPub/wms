using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyConsoleApp.models
{
    public class CustomerInvoice
    {
        [Key]
        public int InvoiceId { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; } = string.Empty;

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;

        [Required]
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;

        public int? DispatchId { get; set; }

        [ForeignKey(nameof(DispatchId))]
        public Dispatch? Dispatch { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        public DateTime? DueDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PaidAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BalanceAmount { get; set; }

        [Required]
        public CustomerInvoiceStatus Status { get; set; } = CustomerInvoiceStatus.Draft;

        [StringLength(50)]
        public string? PaymentTerms { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        public DateTime? SentDate { get; set; }

        public DateTime? FullyPaidDate { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string? CreatedBy { get; set; }

        [StringLength(100)]
        public string? ModifiedBy { get; set; }

        public ICollection<CustomerInvoiceLine> InvoiceLines { get; set; } = new List<CustomerInvoiceLine>();
    }

    public class CustomerInvoiceLine
    {
        [Key]
        public int InvoiceLineId { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        public CustomerInvoice Invoice { get; set; } = null!;

        public int? OrderLineId { get; set; }

        [ForeignKey(nameof(OrderLineId))]
        public OrderLine? OrderLine { get; set; }

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

    public class CustomerPayment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        public CustomerInvoice Invoice { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string PaymentNumber { get; set; } = string.Empty;

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        [StringLength(100)]
        public string? TransactionReference { get; set; }

        [StringLength(100)]
        public string? CheckNumber { get; set; }

        [StringLength(100)]
        public string? CardLastFourDigits { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string? CreatedBy { get; set; }

        [StringLength(100)]
        public string? ModifiedBy { get; set; }

        public DateTime? ProcessedDate { get; set; }

        [StringLength(100)]
        public string? ProcessedBy { get; set; }
    }
}