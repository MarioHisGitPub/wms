using System.Collections.Generic;
using MyConsoleApp.models;

namespace MyConsoleApp.Services
{
    public class AdjustStockService
    {
        // This will store all adjustments in memory for now
        private readonly List<AdjustStock> _adjustments = new();

        /// <summary>
        /// Add a stock adjustment
        /// </summary>
        public void AddAdjustment(AdjustStock adjustment)
        {
            // Basic validation: ProductId and LocationId must be > 0
            if (adjustment.ProductId <= 0 || adjustment.LocationId <= 0)
            {
                throw new System.ArgumentException("Invalid ProductId or LocationId.");
            }

            // Optional: you can also check NewQuantity >= 0
            if (adjustment.NewQuantity < 0)
            {
                throw new System.ArgumentException("Quantity cannot be negative.");
            }

            _adjustments.Add(adjustment);
        }

        /// <summary>
        /// Get all adjustments
        /// </summary>
        public List<AdjustStock> GetAllAdjustments()
        {
            return new List<AdjustStock>(_adjustments);
        }
    }
}
