using EventBus.Events;

namespace Sample3.API.Application.IntegrationEvents.Events
{
    public class OrderPaymentSucceededIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }

        public OrderPaymentSucceededIntegrationEvent(int orderId)
        {
            OrderId = orderId;
        }
    }
}