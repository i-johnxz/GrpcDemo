using EventBus.Events;

namespace Sample3.API.Application.IntegrationEvents.Events
{
    public class OrderStockConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public OrderStockConfirmedIntegrationEvent(int orderId)
        {
            OrderId = orderId;
        }
    }
}