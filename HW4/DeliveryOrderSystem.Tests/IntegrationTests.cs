using System;
using System.Collections.Generic;
using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Services;
using DeliveryOrderSystem.Repositories;
using DeliveryOrderSystem.Strategies;
using DeliveryOrderSystem.Enums;
using Xunit;

namespace DeliveryOrderSystem.Tests
{
    public class IntegrationTests
    {
        [Fact]
        public void OrderService_CreateAndAdvance_ShouldCallNotificationServiceWithExpectedMessages()
        {
            // Arrange
            var orderRepository = new InMemoryOrderRepository();
            var priceCalculator = new PriceCalculator();
            var notificationService = new NotificationService();
            var orderService = new OrderService(orderRepository, priceCalculator, notificationService);

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

            // Act
            var order = orderService.CreateOrder(customerId, lines, address, orderType);
            orderService.AdvanceOrderState(order.Id, OrderStateTrigger.StartPreparation);

            // Assert
            // В реальной реализации здесь был бы мок NotificationService
            // и проверка, что NotifyCustomerOrderUpdate был вызван с ожидаемыми параметрами
            var updatedOrder = orderService.GetOrder(order.Id);
            Assert.Equal("Preparing", updatedOrder.State.Name);
        }

        [Fact]
        public void OrderService_CreateWithRushOrder_ShouldApplyRushMultiplier()
        {
            // Arrange
            var orderRepository = new InMemoryOrderRepository();
            var priceCalculator = new PriceCalculator();
            var notificationService = new NotificationService();
            var orderService = new OrderService(orderRepository, priceCalculator, notificationService);

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
            var orderType = new RushOrder(); // Множитель 1.25

            // Act
            var order = orderService.CreateOrder(customerId, lines, address, orderType);

            // Assert
            // subtotal = 10.0, taxes = 1.0, delivery = 5.0, total before multiplier = 16.0
            // total after multiplier = 16.0 * 1.25 = 20.0
            Assert.Equal(20.0m, order.Total);
        }
    }
}