using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Enums;

namespace DeliveryOrderSystem.StateMachine
{
    public class CancelledState : IOrderState
    {
        public string Name => "Cancelled";

        public void HandleTrigger(Order order, OrderStateTrigger trigger)
        {
            // В состоянии Cancelled нет допустимых переходов
            throw new InvalidOperationException($"No transitions allowed from {Name} state");
        }

        public void OnEnter(Order order)
        {
            // Действия при входе в состояние Cancelled
            // Например, уведомление клиента об отмене
        }

        public void OnExit(Order order)
        {
            // Действия при выходе из состояния Cancelled
            throw new InvalidOperationException($"Cannot exit from {Name} state");
        }
    }
}