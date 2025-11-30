using DeliveryOrderSystem.Interfaces;

namespace DeliveryOrderSystem.Strategies
{
    public class CustomPreferenceOrder : IOrderType
    {
        public string Name => "Custom Preference";
        
        public decimal GetRushMultiplier() => 1.0m;
    }
}