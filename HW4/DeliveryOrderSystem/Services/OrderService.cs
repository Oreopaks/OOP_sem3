using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Enums;

namespace DeliveryOrderSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPriceCalculator _priceCalculator;
        private readonly INotificationService _notificationService;

        public OrderService(IOrderRepository orderRepository, IPriceCalculator priceCalculator, INotificationService notificationService)
        {
            _orderRepository = orderRepository;
            _priceCalculator = priceCalculator;
            _notificationService = notificationService;
        }

        public Order CreateOrder(Guid customerId, IEnumerable<(Guid menuItemId, int qty, IEnumerable<Modifier> modifiers)> lines, Address address, IOrderType orderType)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                DeliveryAddress = address,
                OrderType = orderType,
                CreatedAt = DateTime.UtcNow
            };

            foreach (var (menuItemId, qty, modifiers) in lines)
            {
                // В реальной реализации здесь был бы репозиторий меню
                // Для упрощения создаем MenuItem напрямую
                var menuItem = new MenuItem
                {
                    Id = menuItemId,
                    Name = $"Menu Item {menuItemId}",
                    Price = 10.0m, // Цена по умолчанию для примера
                    IsAvailable = true
                };

                var orderLine = new OrderLine
                {
                    Item = menuItem,
                    Quantity = qty,
                    Modifiers = modifiers.ToList()
                };

                order.Lines.Add(orderLine);
            }

            // Рассчитываем стоимость заказа
            order.SubTotal = _priceCalculator.CalculateSubtotal(order);
            order.DeliveryFee = _priceCalculator.CalculateDeliveryFee(order);
            order.Taxes = _priceCalculator.CalculateTaxes(order.SubTotal, order.DeliveryAddress);
            order.Discounts = _priceCalculator.CalculateDiscounts(order);
            order.Total = _priceCalculator.CalculateTotal(order);

            _orderRepository.Add(order);
            _notificationService.NotifyCustomerOrderUpdate(customerId, $"Order {order.Id} created successfully.");

            return order;
        }

        public void UpdateOrder(Guid orderId, Action<Order> update)
        {
            var order = _orderRepository.Get(orderId);
            if (order == null)
                throw new ArgumentException("Order not found");

            update(order);

            // Пересчитываем стоимость после обновления
            order.SubTotal = _priceCalculator.CalculateSubtotal(order);
            order.DeliveryFee = _priceCalculator.CalculateDeliveryFee(order);
            order.Taxes = _priceCalculator.CalculateTaxes(order.SubTotal, order.DeliveryAddress);
            order.Discounts = _priceCalculator.CalculateDiscounts(order);
            order.Total = _priceCalculator.CalculateTotal(order);

            _orderRepository.Update(order);
            _notificationService.NotifyCustomerOrderUpdate(order.CustomerId, $"Order {order.Id} updated.");
        }

        public void CancelOrder(Guid orderId)
        {
            var order = _orderRepository.Get(orderId);
            if (order == null)
                throw new ArgumentException("Order not found");

            // Создаем триггер отмены для текущего состояния
            order.State.HandleTrigger(order, OrderStateTrigger.Cancel);
            
            _orderRepository.Update(order);
            _notificationService.NotifyCustomerOrderUpdate(order.CustomerId, $"Order {order.Id} cancelled.");
        }

        public void AdvanceOrderState(Guid orderId, OrderStateTrigger trigger)
        {
            var order = _orderRepository.Get(orderId);
            if (order == null)
                throw new ArgumentException("Order not found");

            order.State.HandleTrigger(order, trigger);
            
            _orderRepository.Update(order);
            _notificationService.NotifyCustomerOrderUpdate(order.CustomerId, $"Order {order.Id} state changed to {order.State.Name}.");
        }

        public Order GetOrder(Guid orderId)
        {
            var order = _orderRepository.Get(orderId);
            if (order == null)
                throw new ArgumentException("Order not found");
            return order;
        }

        public IEnumerable<Order> GetOrdersByCustomer(Guid customerId)
        {
            return _orderRepository.Query(o => o.CustomerId == customerId);
        }
    }
}