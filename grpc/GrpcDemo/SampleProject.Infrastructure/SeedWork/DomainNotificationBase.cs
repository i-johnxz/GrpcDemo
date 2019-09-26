﻿using System.Text.Json.Serialization;
using MediatR;

namespace SampleProject.Infrastructure.SeedWork
{
    public class DomainNotificationBase<T> : IDomainEventNotification<T>, INotification
    {
        [JsonIgnore]
        public T DomainEvent { get; }

        public DomainNotificationBase(T domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }
}