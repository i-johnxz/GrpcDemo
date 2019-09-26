using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders.Events
{
    public class OrderRemovedEvent: DomainEventBase
    {
        public Order Order { get; set; }

        public OrderRemovedEvent(Order order)
        {
            Order = order;
        }
    }
}