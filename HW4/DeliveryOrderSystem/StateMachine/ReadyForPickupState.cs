using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Enums;

namespace DeliveryOrderSystem.StateMachine
{
    public class ReadyForPickupState : IOrderState
    {
        public string Name => "ReadyForPickup";

        public void HandleTrigger(Order order, OrderStateTrigger trigger)
        {
            switch (trigger)
            {
                case OrderStateTrigger.AssignCourier:
                    OnExit(order);
                    order.State = new OutForDeliveryState();
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
            // Действия при входе в состояние ReadyForPickup
        }

        public void OnExit(Order order)
        {
            // Действия при выходе из состояния ReadyForPickup
        }
    }
}