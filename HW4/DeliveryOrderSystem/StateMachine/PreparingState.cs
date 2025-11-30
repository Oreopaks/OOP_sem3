using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Enums;

namespace DeliveryOrderSystem.StateMachine
{
    public class PreparingState : IOrderState
    {
        public string Name => "Preparing";

        public void HandleTrigger(Order order, OrderStateTrigger trigger)
        {
            switch (trigger)
            {
                case OrderStateTrigger.ReadyForPickup:
                    OnExit(order);
                    order.State = new ReadyForPickupState();
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
            // Действия при входе в состояние Preparing
            // Например, уведомление персонала о начале приготовления
        }

        public void OnExit(Order order)
        {
            // Действия при выходе из состояния Preparing
        }
    }
}