using System.Collections.Generic;
using BuildingBlocks.Domain;

namespace BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    public interface IDomainEventsAccessor
    {
        List<IDomainEvent> GetAllDomainEvents();

        void ClearAllDomainEvents();
    }
}
