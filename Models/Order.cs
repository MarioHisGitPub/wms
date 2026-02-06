namespace MyConsoleApp.models
{
    public class Order
    {
        public int Id { get; set; }

        public string Status { get; set; } = "Pending";

        public ICollection<OrderLine> OrderLines { get; set; }
            = new List<OrderLine>();
    }
}
