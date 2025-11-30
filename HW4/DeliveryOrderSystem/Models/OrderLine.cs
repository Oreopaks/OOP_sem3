using DeliveryOrderSystem.Models;

namespace DeliveryOrderSystem.Models
{
    public class OrderLine
    {
        public MenuItem Item { get; set; } = new MenuItem();
        public int Quantity { get; set; }
        public List<Modifier> Modifiers { get; set; } = new List<Modifier>();
    }
}