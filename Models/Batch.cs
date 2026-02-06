namespace MyConsoleApp.models
{
    public class Batch
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public DateTime Expiration { get; set; }
        public int LocationId { get; set; }
        public Location? Location { get; set; }
    }
}