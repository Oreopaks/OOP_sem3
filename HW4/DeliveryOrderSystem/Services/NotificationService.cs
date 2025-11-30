using DeliveryOrderSystem.Interfaces;
using DeliveryOrderSystem.Models;

namespace DeliveryOrderSystem.Services
{
    public class NotificationService : INotificationService
    {
        public void NotifyCustomerOrderUpdate(Guid customerId, string message)
        {
            // В реальной системе здесь была бы отправка уведомлений
            // через SMS, email, push-уведомления и т.д.
            Console.WriteLine($"[NOTIFICATION] Customer {customerId}: {message}");
        }

        public void NotifyCourier(Order order)
        {
            // В реальной системе здесь была бы отправка уведомлений курьеру
            Console.WriteLine($"[NOTIFICATION] Courier notified for order {order.Id}");
        }
    }
}