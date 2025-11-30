namespace DeliveryOrderSystem.Interfaces
{
    public interface IOrderType 
    { 
        decimal GetRushMultiplier(); 
        string Name { get; } 
    }
}