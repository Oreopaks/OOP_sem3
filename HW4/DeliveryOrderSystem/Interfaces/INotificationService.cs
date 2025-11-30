using DeliveryOrderSystem.Models;

namespace DeliveryOrderSystem.Interfaces
{
    public interface INotificationService
    {
        void NotifyCustomerOrderUpdate(Guid customerId, string message);
        void NotifyCourier(Order order);
    }
}