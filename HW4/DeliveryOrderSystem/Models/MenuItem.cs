namespace DeliveryOrderSystem.Models
{
    public class MenuItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public IEnumerable<string> Ingredients { get; set; } = new List<string>();
        public bool IsAvailable { get; set; }
    }
}