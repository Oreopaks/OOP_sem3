using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;

namespace DeliveryOrderSystem.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly Dictionary<Guid, Order> _orders = new Dictionary<Guid, Order>();

        public void Add(Order order)
        {
            _orders[order.Id] = order;
        }

        public void Update(Order order)
        {
            if (_orders.ContainsKey(order.Id))
            {
                _orders[order.Id] = order;
            }
        }

        public Order? Get(Guid id)
        {
            _orders.TryGetValue(id, out var order);
            return order;
        }

        public IEnumerable<Order> Query(Func<Order, bool> predicate)
        {
            return _orders.Values.Where(predicate);
        }
    }
}