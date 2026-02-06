namespace MyConsoleApp.models
{
    public class Location
    {
        public int Id { get; set; }
        public string Aisle { get; set; } = string.Empty;
        public string Rack { get; set; } = string.Empty;
        public string Shelf { get; set; } = string.Empty;
        public string Bin { get; set; } = string.Empty;
        public int ItemsStored { get; set; }
    }
}