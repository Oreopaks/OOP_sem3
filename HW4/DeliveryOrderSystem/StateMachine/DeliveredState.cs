using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Enums;

namespace DeliveryOrderSystem.StateMachine
{
    public class DeliveredState : IOrderState
    {
        public string Name => "Delivered";

        public void HandleTrigger(Order order, OrderStateTrigger trigger)
        {
            switch (trigger)
            {
                case OrderStateTrigger.Complete:
                    OnExit(order);
                    order.State = new CompletedState();
                    order.State.OnEnter(order);
                    break;
                case OrderStateTrigger.Cancel:
                    // Возврат после доставки может иметь особую логику
                    OnExit(order);
                    order.State = new CancelledState();
                    order.State.OnEnter(order);
                    break;
                default:
                    throw new InvalidOperationException($"Trigger {trigger} not valid for {Name} state");
            }
        }

        public void OnEnter(Order order)
        {
            // Действия при входе в состояние Delivered
            // Например, уведомление клиента о доставке
        }

        public void OnExit(Order order)
        {
            // Действия при выходе из состояния Delivered
        }
    }
}