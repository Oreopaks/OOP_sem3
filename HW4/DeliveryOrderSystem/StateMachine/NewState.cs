using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Enums;

namespace DeliveryOrderSystem.StateMachine
{
    public class NewState : IOrderState
    {
        public string Name => "New";

        public void HandleTrigger(Order order, OrderStateTrigger trigger)
        {
            switch (trigger)
            {
                case OrderStateTrigger.StartPreparation:
                    OnExit(order);
                    order.State = new PreparingState();
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
            // Действия при входе в состояние New
        }

        public void OnExit(Order order)
        {
            // Действия при выходе из состояния New
        }
    }
}