using System;
using MediatR;
using Sample3.Domain.AggregatesModel.OrderAggregate;

namespace Sample3.Domain.Events
{
    public class OrderCancelledDomainEvent : INotification
    {
        public Order Order { get; }
        public OrderCancelledDomainEvent(Order order)
        {
            Order = order;
        }
    }
}