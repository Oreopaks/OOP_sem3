using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Enums;

namespace DeliveryOrderSystem.StateMachine
{
    public class OutForDeliveryState : IOrderState
    {
        public string Name => "OutForDelivery";

        public void HandleTrigger(Order order, OrderStateTrigger trigger)
        {
            switch (trigger)
            {
                case OrderStateTrigger.Deliver:
                    OnExit(order);
                    order.State = new DeliveredState();
                    order.State.OnEnter(order);
                    break;
                case OrderStateTrigger.Cancel:
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
            // Действия при входе в состояние OutForDelivery
            // Например, уведомление курьера
        }

        public void OnExit(Order order)
        {
            // Действия при выходе из состояния OutForDelivery
        }
    }
}