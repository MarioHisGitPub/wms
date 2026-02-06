namespace MyConsoleApp.models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = "Operator";
        public DateTime LastLogin { get; set; } = DateTime.Now;
    }
}