using System;
using System.Collections.Generic;
using MediatR;
using Sample3.Domain.AggregatesModel.OrderAggregate;

namespace Sample3.Domain.Events
{
    public class OrderStatusChangedToPaidDomainEvent
        : INotification
    {
        public int OrderId { get; }
        public IEnumerable<OrderItem> OrderItems { get; }

        public OrderStatusChangedToPaidDomainEvent(int orderId,
            IEnumerable<OrderItem> orderItems)
        {
            OrderId = orderId;
            OrderItems = orderItems;
        }
    }
}