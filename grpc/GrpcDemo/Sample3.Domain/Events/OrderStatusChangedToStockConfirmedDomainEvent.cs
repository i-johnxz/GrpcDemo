﻿using System;
using MediatR;

namespace Sample3.Domain.Events
{
    public class OrderStatusChangedToStockConfirmedDomainEvent : INotification
    {
        
        public int OrderId { get; }
        public OrderStatusChangedToStockConfirmedDomainEvent(int orderId) => OrderId = orderId;
    }
}