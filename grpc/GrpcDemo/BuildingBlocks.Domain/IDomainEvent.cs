using System;
using MediatR;

namespace BuildingBlocks.Domain
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}
