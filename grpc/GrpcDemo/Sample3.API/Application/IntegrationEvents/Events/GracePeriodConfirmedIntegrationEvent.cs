using EventBus.Events;

namespace Sample3.API.Application.IntegrationEvents.Events
{
    public class GracePeriodConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public GracePeriodConfirmedIntegrationEvent(int orderId)
        {
            OrderId = orderId;
        }
    }
}