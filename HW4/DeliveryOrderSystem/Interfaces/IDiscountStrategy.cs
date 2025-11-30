using DeliveryOrderSystem.Models;

namespace DeliveryOrderSystem.Interfaces
{
    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(Order order, decimal currentTotal);
    }
}