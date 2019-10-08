using EventBus.Events;

namespace Sample3.API.Application.IntegrationEvents.Events
{
    public class OrderStatusChangedToCancelledIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }

        public string OrderStatus { get; set; }

        public string BuyerName { get; set; }

        public OrderStatusChangedToCancelledIntegrationEvent(int orderId, string orderStatus, string buyerName)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
            BuyerName = buyerName;
        }
    }
}