using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyConsoleApp.models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Code { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? State { get; set; }

        [StringLength(20)]
        public string? PostalCode { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(50)]
        public string? Phone { get; set; }

        [StringLength(50)]
        public string? Mobile { get; set; }

        [StringLength(50)]
        public string? TaxId { get; set; }

        [StringLength(50)]
        public string? PaymentTerms { get; set; }

        [StringLength(3)]
        public string? CurrencyCode { get; set; } = "USD";

        public decimal? CreditLimit { get; set; }

        public bool IsActive { get; set; } = true;

        [StringLength(500)]
        public string? Notes { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string? CreatedBy { get; set; }

        [StringLength(100)]
        public string? ModifiedBy { get; set; }

        // Navigation properties
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}