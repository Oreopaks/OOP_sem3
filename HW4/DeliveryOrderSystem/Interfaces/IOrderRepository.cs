using DeliveryOrderSystem.Models;

namespace DeliveryOrderSystem.Interfaces
{
    public interface IOrderRepository
    {
        void Add(Order order);
        void Update(Order order);
        Order? Get(Guid id);
        IEnumerable<Order> Query(Func<Order, bool> predicate);
    }
}