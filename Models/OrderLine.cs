namespace MyConsoleApp.models
{
    public class OrderLine
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }

        // Optional extras you can add later
        // public decimal UnitPriceAtOrder { get; set; }
        // public decimal Subtotal => Quantity * UnitPriceAtOrder;
    }
}