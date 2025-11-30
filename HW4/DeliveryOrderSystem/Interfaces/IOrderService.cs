using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.Enums;

namespace DeliveryOrderSystem.Interfaces
{
    public interface IOrderService
    {
        Order CreateOrder(Guid customerId, IEnumerable<(Guid menuItemId, int qty, IEnumerable<Modifier> modifiers)> lines, Address address, IOrderType orderType);
        void UpdateOrder(Guid orderId, Action<Order> update);
        void CancelOrder(Guid orderId);
        void AdvanceOrderState(Guid orderId, OrderStateTrigger trigger);
        Order GetOrder(Guid orderId);
        IEnumerable<Order> GetOrdersByCustomer(Guid customerId);
    }
}