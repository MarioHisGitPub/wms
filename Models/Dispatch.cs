namespace MyConsoleApp.models
{
    public class Dispatch
    {
        public int Id { get; set; }
        public int PickingId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Destination { get; set; } = "";
        public string Status { get; set; } = "";

        // Navigation properties
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
