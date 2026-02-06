namespace MyConsoleApp.models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
}