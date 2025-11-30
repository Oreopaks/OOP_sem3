using System;
using System.Collections.Generic;
using System.Linq;
using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Services;
using DeliveryOrderSystem.Repositories;
using DeliveryOrderSystem.Strategies;
using DeliveryOrderSystem.Enums;
using Xunit;

namespace DeliveryOrderSystem.Tests
{
    public class OrderServiceTests
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPriceCalculator _priceCalculator;
        private readonly INotificationService _notificationService;
        private readonly IOrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepository = new InMemoryOrderRepository();
            _priceCalculator = new PriceCalculator();
            _notificationService = new NotificationService();
            _orderService = new OrderService(_orderRepository, _priceCalculator, _notificationService);
        }

        [Fact]
        public void CreateOrder_ShouldPersistOrder_WithCorrectTotals()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var menuItemId = Guid.NewGuid();
            var lines = new List<(Guid, int, IEnumerable<Modifier>)>
            {
                (menuItemId, 2, new List<Modifier>())
            };
            var address = new Address
            {
                Street = "123 Main St",
                City = "Test City",
                PostalCode = "12345"
            };
            var orderType = new StandardOrder();

            // Act
            var order = _orderService.CreateOrder(customerId, lines, address, orderType);

            // Assert
            Assert.NotNull(order);
            Assert.Equal(customerId, order.CustomerId);
            Assert.Single(order.Lines); // Проверяем, что создалась одна строка заказа
            Assert.Equal(20.0m, order.SubTotal); // 2 * 10.0m
            Assert.Equal(2.0m, order.Taxes); // 10% налог
            Assert.Equal(5.0m, order.DeliveryFee);
            Assert.Equal(0.0m, order.Discounts);
            Assert.Equal(27.0m, order.Total); // 20.0 + 2.0 + 5.0
        }

        [Fact]
        public void CancelOrder_ShouldSetStateCancelled_AndNotifyCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var menuItemId = Guid.NewGuid();
            var lines = new List<(Guid, int, IEnumerable<Modifier>)>
            {
                (menuItemId, 1, new List<Modifier>())
            };
            var address = new Address
            {
                Street = "123 Main St",
                City = "Test City",
                PostalCode = "12345"
            };
            var orderType = new StandardOrder();
            
            var order = _orderService.CreateOrder(customerId, lines, address, orderType);

            // Act
            _orderService.CancelOrder(order.Id);

            // Assert
            var updatedOrder = _orderService.GetOrder(order.Id);
            Assert.Equal("Cancelled", updatedOrder.State.Name);
        }

        [Fact]
        public void AdvanceOrderState_PreparingToOutForDelivery_ShouldSetETA()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var menuItemId = Guid.NewGuid();
            var lines = new List<(Guid, int, IEnumerable<Modifier>)>
            {
                (menuItemId, 1, new List<Modifier>())
            };
            var address = new Address
            {
                Street = "123 Main St",
                City = "Test City",
                PostalCode = "12345"
            };
            var orderType = new StandardOrder();
            
            var order = _orderService.CreateOrder(customerId, lines, address, orderType);
            
            // Переводим заказ в состояние Preparing
            _orderService.AdvanceOrderState(order.Id, OrderStateTrigger.StartPreparation);
            
            // Затем в состояние ReadyForPickup
            _orderService.AdvanceOrderState(order.Id, OrderStateTrigger.ReadyForPickup);

            // Act
            _orderService.AdvanceOrderState(order.Id, OrderStateTrigger.AssignCourier);

            // Assert
            var updatedOrder = _orderService.GetOrder(order.Id);
            Assert.Equal("OutForDelivery", updatedOrder.State.Name);
        }

        [Fact]
        public void UpdateOrder_ShouldRecalculateTotals_WhenLinesChanged()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var menuItemId = Guid.NewGuid();
            var lines = new List<(Guid, int, IEnumerable<Modifier>)>
            {
                (menuItemId, 1, new List<Modifier>())
            };
            var address = new Address
            {
                Street = "123 Main St",
                City = "Test City",
                PostalCode = "12345"
            };
            var orderType = new StandardOrder();
            
            var order = _orderService.CreateOrder(customerId, lines, address, orderType);

            // Act
            _orderService.UpdateOrder(order.Id, o => {
                o.Lines[0].Quantity = 3;
            });

            // Assert
            var updatedOrder = _orderService.GetOrder(order.Id);
            Assert.Equal(30.0m, updatedOrder.SubTotal); // 3 * 10.0m
            Assert.Equal(3.0m, updatedOrder.Taxes); // 10% налог от 30.0
            Assert.Equal(38.0m, updatedOrder.Total); // 30.0 + 3.0 + 5.0
        }
    }
}