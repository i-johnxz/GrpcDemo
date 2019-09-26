﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using MediatR;
using SampleProject.Domain.SeedWork;
using SampleProject.Infrastructure.Outbox;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.Infrastructure
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;

        private readonly ILifetimeScope _scope;

        private readonly OrdersContext _ordersContext;
        
        
        
        public DomainEventsDispatcher(IMediator mediator, ILifetimeScope scope, OrdersContext ordersContext)
        {
            _mediator = mediator;
            _scope = scope;
            _ordersContext = ordersContext;
        }

        
        public async Task DispatchEventsAsync()
        {
            var domainEntities = this._ordersContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();

            foreach (var domainEvent in domainEvents)
            {
                Type domainEvenNotificationType = typeof(IDomainEventNotification<>);
                var domainNotificationWithGenericType =
                    domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
                var domainNotification = _scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
                {
                    new NamedPropertyParameter("domainEvent", domainEvent)
                });

                if (domainNotification != null)
                {
                    domainEventNotifications.Add(domainNotification as SeedWork.IDomainEventNotification<IDomainEvent>);
                }
            }
            
            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents.Select(async (domainEvent) => { await _mediator.Publish(domainEvent); });

            await Task.WhenAll(tasks);

            foreach (var domainEventNotification in domainEventNotifications)
            {
                string type = domainEventNotification.GetType().FullName;
                var data = JsonSerializer.Serialize(domainEventNotification);
                OutboxMessage outboxMessage = new OutboxMessage(
                    domainEventNotification.DomainEvent.OccurredOn,
                    type,
                    data);
                this._ordersContext.OutboxMessages.Add(outboxMessage);
            }
        }
    }
}