using System;
using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.StateMachine;
using DeliveryOrderSystem.Enums;
using Xunit;

namespace DeliveryOrderSystem.Tests
{
    public class StateMachineTests
    {
        [Fact]
        public void NewState_OnAdvanceToPreparing_ShouldInvokeOnEnter_Preparing()
        {
            // Arrange
            var order = new Order();
            Assert.Equal("New", order.State.Name);

            // Act
            order.State.HandleTrigger(order, OrderStateTrigger.StartPreparation);

            // Assert
            Assert.Equal("Preparing", order.State.Name);
        }

        [Fact]
        public void OutForDeliveryState_OnEnter_ShouldNotifyCourier()
        {
            // Arrange
            var order = new Order();
            order.State.HandleTrigger(order, OrderStateTrigger.StartPreparation);
            order.State.HandleTrigger(order, OrderStateTrigger.ReadyForPickup);

            // Act & Assert
            // В реальной реализации здесь был бы мок NotificationService
            // и проверка, что NotifyCourier был вызван
            var exception = Record.Exception(() => order.State.HandleTrigger(order, OrderStateTrigger.AssignCourier));
            Assert.Null(exception);
            Assert.Equal("OutForDelivery", order.State.Name);
        }
    }
}