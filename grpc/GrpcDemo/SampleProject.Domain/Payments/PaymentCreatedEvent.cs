using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Payments
{
    public class PaymentCreatedEvent : DomainEventBase
    {
        public PaymentId PaymentId { get; set; }

        public OrderId OrderId { get; set; }


        public PaymentCreatedEvent(PaymentId paymentId, OrderId orderId)
        {
            PaymentId = paymentId;
            OrderId = orderId;
        }
    }
}