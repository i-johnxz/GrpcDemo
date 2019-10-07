using System;
using System.Threading.Tasks;
using EventBus.Events;

namespace Sample3.API.Application.IntegrationEvents
{
    public interface IOrderingIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}