using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Enums;

namespace DeliveryOrderSystem.Interfaces
{
    public interface IOrderState
    {
        void HandleTrigger(Order order, OrderStateTrigger trigger);
        string Name { get; }
        void OnEnter(Order order);
        void OnExit(Order order);
    }
}