﻿using System.Collections.Generic;

namespace BuildingBlocks.Domain
{
    public abstract class Entity
    {
        private List<IDomainEvent> _domainEvents;

        /// <summary>
        /// Domain events occurred.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();


        /// <summary>
        /// Add domain event.
        /// </summary>
        /// <param name="domainEvent"></param>
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= new List<IDomainEvent>();
            this._domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// remove domain event.
        /// </summary>
        /// <param name="domainEvent"></param>
        protected void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            this._domainEvents?.Remove(domainEvent);
        }



        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        protected void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}
