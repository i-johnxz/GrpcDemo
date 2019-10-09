using System.Threading.Tasks;

namespace BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}
