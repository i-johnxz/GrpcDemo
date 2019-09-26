using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders.Events
{
    public class OrderPlacedEvent: DomainEventBase
    {
        public OrderId OrderId { get; set; }

        public OrderPlacedEvent(OrderId orderId)
        {
            OrderId = orderId;
        }
        
    }
}