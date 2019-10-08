using EventBus.Events;

namespace Sample3.API.Application.IntegrationEvents.Events
{
    public class OrderStartedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; }

        public OrderStartedIntegrationEvent(string userId)
        {
            UserId = userId;
        }
    }
}