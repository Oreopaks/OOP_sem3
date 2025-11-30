using DeliveryOrderSystem.Interfaces;

namespace DeliveryOrderSystem.Strategies
{
    public class StandardOrder : IOrderType
    {
        public string Name => "Standard";
        
        public decimal GetRushMultiplier() => 1.0m;
    }
}