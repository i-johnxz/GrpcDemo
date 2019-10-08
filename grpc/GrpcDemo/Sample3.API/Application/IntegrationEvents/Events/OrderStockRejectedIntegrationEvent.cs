using System.Collections.Generic;
using EventBus.Events;

namespace Sample3.API.Application.IntegrationEvents.Events
{
    public class OrderStockRejectedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public List<ConfirmedOrderStockItem> OrderStockItems { get; }

        public OrderStockRejectedIntegrationEvent(int orderId, List<ConfirmedOrderStockItem> orderStockItems)
        {
            OrderId = orderId;
            OrderStockItems = orderStockItems;
        }
    }

    public class ConfirmedOrderStockItem
    {
        public int ProductId { get; }

        public bool HasStock { get; set; }


        public ConfirmedOrderStockItem(int productId, bool hasStock)
        {
            ProductId = productId;
            HasStock = hasStock;
        }
    }
}