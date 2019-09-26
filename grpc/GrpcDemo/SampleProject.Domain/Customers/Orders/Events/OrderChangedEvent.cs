using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders.Events
{
    public class OrderChangedEvent : DomainEventBase
    {
        public Order Order { get; }


        public OrderChangedEvent(Order order)
        {
            Order = order;
        }
        
    }
}