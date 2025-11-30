using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Enums;

namespace DeliveryOrderSystem.StateMachine
{
    public class CompletedState : IOrderState
    {
        public string Name => "Completed";

        public void HandleTrigger(Order order, OrderStateTrigger trigger)
        {
            // В состоянии Completed нет допустимых переходов
            throw new InvalidOperationException($"No transitions allowed from {Name} state");
        }

        public void OnEnter(Order order)
        {
            // Действия при входе в состояние Completed
            // Например, начисление бонусных баллов
        }

        public void OnExit(Order order)
        {
            // Действия при выходе из состояния Completed
            throw new InvalidOperationException($"Cannot exit from {Name} state");
        }
    }
}