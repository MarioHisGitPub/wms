namespace MyConsoleApp.models
{
    public class Receiving
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public DateTime DateReceived { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Received";
        public int? LocationId { get; set; }
        public Location? Location { get; set; }
    }
}