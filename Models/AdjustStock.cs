namespace MyConsoleApp.models
{
    public class AdjustStock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int LocationId { get; set; }
        public Location? Location { get; set; }
        public int OldQuantity { get; set; }
        public int NewQuantity { get; set; }
        public string Reason { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateTime AdjustedAt { get; set; } = DateTime.Now;
    }
}