namespace MyConsoleApp.models
{
    public class Report
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime GeneratedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Complete";
    }
}