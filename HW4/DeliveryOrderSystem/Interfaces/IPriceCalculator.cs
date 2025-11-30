using DeliveryOrderSystem.Models;

namespace DeliveryOrderSystem.Interfaces
{
    public interface IPriceCalculator
    {
        decimal CalculateSubtotal(Order order);
        decimal CalculateTaxes(decimal subtotal, Address address);
        decimal CalculateDeliveryFee(Order order);
        decimal CalculateDiscounts(Order order);
        decimal CalculateTotal(Order order);
    }
}