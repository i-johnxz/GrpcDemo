using System.Collections.Generic;
using EventBus.Events;

namespace Sample3.API.Application.IntegrationEvents.Events
{
    public class OrderStatusChangedToAwaitingValidationIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public string OrderStatus { get; }

        public string BuyerName { get;  }

        public IEnumerable<OrderStockItem> OrderStockItems { get; }

        public OrderStatusChangedToAwaitingValidationIntegrationEvent(
            int orderId, 
            string orderStatus, 
            string buyerName, 
            IEnumerable<OrderStockItem> orderStockItems)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
            BuyerName = buyerName;
            OrderStockItems = orderStockItems;
        }
        
    }


    public class OrderStockItem
    {
        public int ProductId { get; }

        public int Units { get; }

        public OrderStockItem(int productId, int units)
        {
            ProductId = productId;
            Units = units;
        }
        
    }
}