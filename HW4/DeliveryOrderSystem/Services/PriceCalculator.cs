using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;

namespace DeliveryOrderSystem.Services
{
    public class PriceCalculator : IPriceCalculator
    {
        public decimal CalculateSubtotal(Order order)
        {
            decimal subtotal = 0;
            foreach (var line in order.Lines)
            {
                subtotal += line.Item.Price * line.Quantity;
                foreach (var modifier in line.Modifiers)
                {
                    subtotal += modifier.AdditionalCost * line.Quantity;
                }
            }
            return subtotal;
        }

        public decimal CalculateTaxes(decimal subtotal, Address address)
        {
            // Простая реализация - налог 10% для всех заказов
            // В реальной системе это зависело бы от региона
            return subtotal * 0.1m;
        }

        public decimal CalculateDeliveryFee(Order order)
        {
            // Базовая стоимость доставки 5.0
            // В реальной системе это зависело бы от расстояния, веса и других факторов
            return 5.0m;
        }

        public decimal CalculateDiscounts(Order order)
        {
            // Пока без скидок
            // В реальной системе здесь применялись бы различные стратегии скидок
            return 0.0m;
        }

        public decimal CalculateTotal(Order order)
        {
            var subtotal = CalculateSubtotal(order);
            var taxes = CalculateTaxes(subtotal, order.DeliveryAddress);
            var deliveryFee = CalculateDeliveryFee(order);
            var discounts = CalculateDiscounts(order);
            
            var total = subtotal + taxes + deliveryFee - discounts;
            
            // Применяем множитель типа заказа (например, срочный заказ)
            total *= order.OrderType.GetRushMultiplier();
            
            return total;
        }
    }
}