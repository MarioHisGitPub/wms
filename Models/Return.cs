namespace MyConsoleApp.models
{
    public class Return
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public int? LocationId { get; set; }
        public Location? Location { get; set; }
    }
}