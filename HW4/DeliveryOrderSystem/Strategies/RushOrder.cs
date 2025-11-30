using DeliveryOrderSystem.Interfaces;

namespace DeliveryOrderSystem.Strategies
{
    public class RushOrder : IOrderType
    {
        public string Name => "Rush";
        
        public decimal GetRushMultiplier() => 1.25m;
    }
}