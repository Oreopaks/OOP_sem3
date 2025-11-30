using System;
using System.Linq;
using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Repositories;
using Xunit;

namespace DeliveryOrderSystem.Tests
{
    public class RepositoryTests
    {
        [Fact]
        public void InMemoryOrderRepository_AddGet_ShouldReturnSameOrder()
        {
            // Arrange
            var repository = new InMemoryOrderRepository();
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid()
            };

            // Act
            repository.Add(order);
            var retrievedOrder = repository.Get(order.Id);

            // Assert
            Assert.NotNull(retrievedOrder);
            Assert.Equal(order.Id, retrievedOrder.Id);
            Assert.Equal(order.CustomerId, retrievedOrder.CustomerId);
        }

        [Fact]
        public void Repository_Query_ShouldReturnMatchingOrders()
        {
            // Arrange
            var repository = new InMemoryOrderRepository();
            var customerId = Guid.NewGuid();
            
            var order1 = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId
            };
            
            var order2 = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid() // Другой клиент
            };
            
            repository.Add(order1);
            repository.Add(order2);

            // Act
            var customerOrders = repository.Query(o => o.CustomerId == customerId);

            // Assert
            Assert.Single(customerOrders);
            Assert.Equal(order1.Id, customerOrders.First().Id);
        }
    }
}