using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace SampleProject.Domain.SeedWork
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}
