using System;
using System.Collections.Generic;
using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Services;
using DeliveryOrderSystem.Strategies;
using Xunit;

namespace DeliveryOrderSystem.Tests
{
    public class PriceCalculatorTests
    {
        private readonly PriceCalculator _priceCalculator;

        public PriceCalculatorTests()
        {
            _priceCalculator = new PriceCalculator();
        }

        [Fact]
        public void CalculateTotal_ShouldIncludeTaxesAndDeliveryAndApplyDiscounts()
        {
            // Arrange
            var order = new Order
            {
                Lines = new List<OrderLine>
                {
                    new OrderLine
                    {
                        Item = new MenuItem { Price = 10.0m },
                        Quantity = 2
                    }
                },
                DeliveryAddress = new Address(),
                OrderType = new StandardOrder()
            };

            // Act
            var subtotal = _priceCalculator.CalculateSubtotal(order);
            var taxes = _priceCalculator.CalculateTaxes(subtotal, order.DeliveryAddress);
            var deliveryFee = _priceCalculator.CalculateDeliveryFee(order);
            var discounts = _priceCalculator.CalculateDiscounts(order);
            var total = _priceCalculator.CalculateTotal(order);

            // Assert
            Assert.Equal(20.0m, subtotal);
            Assert.Equal(2.0m, taxes);
            Assert.Equal(5.0m, deliveryFee);
            Assert.Equal(0.0m, discounts);
            Assert.Equal(27.0m, total);
        }

        [Fact]
        public void CalculateDeliveryFee_ShouldUseDistanceAndPeakTimeRules()
        {
            // Arrange
            var order = new Order
            {
                Lines = new List<OrderLine>
                {
                    new OrderLine
                    {
                        Item = new MenuItem { Price = 10.0m },
                        Quantity = 1
                    }
                },
                DeliveryAddress = new Address(),
                OrderType = new StandardOrder()
            };

            // Act
            var deliveryFee = _priceCalculator.CalculateDeliveryFee(order);

            // Assert
            Assert.Equal(5.0m, deliveryFee);
        }

        [Fact]
        public void DecoratorRushFee_ShouldIncreaseTotal_ByExpectedMultiplier()
        {
            // Arrange
            var order = new Order
            {
                Lines = new List<OrderLine>
                {
                    new OrderLine
                    {
                        Item = new MenuItem { Price = 10.0m },
                        Quantity = 1
                    }
                },
                DeliveryAddress = new Address(),
                OrderType = new RushOrder() // Множитель 1.25
            };

            // Act
            var total = _priceCalculator.CalculateTotal(order);

            // Assert
            // subtotal = 10.0, taxes = 1.0, delivery = 5.0, total before multiplier = 16.0
            // total after multiplier = 16.0 * 1.25 = 20.0
            Assert.Equal(20.0m, total);
        }
    }
}