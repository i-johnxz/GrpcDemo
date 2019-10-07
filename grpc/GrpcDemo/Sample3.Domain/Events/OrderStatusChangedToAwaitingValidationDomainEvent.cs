using System;
using System.Collections.Generic;
using MediatR;
using Sample3.Domain.AggregatesModel.OrderAggregate;

namespace Sample3.Domain.Events
{
    public class OrderStatusChangedToAwaitingValidationDomainEvent : INotification
    {
        
        public int OrderOrderId { get; }
        public IEnumerable<OrderItem> OrderItems { get; }
        public OrderStatusChangedToAwaitingValidationDomainEvent(int orderId, IEnumerable<OrderItem> orderItems)
        {
            OrderOrderId = orderId;
            OrderItems = orderItems;
        }
    }
}