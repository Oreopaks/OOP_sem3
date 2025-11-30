using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;
using DeliveryOrderSystem.StateMachine;
using DeliveryOrderSystem.Strategies;

namespace DeliveryOrderSystem.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderLine> Lines { get; set; } = new List<OrderLine>();
        public IOrderType OrderType { get; set; } = new StandardOrder();
        public IOrderState State { get; set; } = new NewState();
        public Address DeliveryAddress { get; set; } = new Address();
        public decimal SubTotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Taxes { get; set; }
        public decimal Discounts { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EstimatedDeliveryAt { get; set; }
    }
}