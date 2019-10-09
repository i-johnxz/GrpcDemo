using System;
using BuildingBlocks.Domain;
using Sample4.Domain.Accounts;

namespace Sample4.Domain.Customers
{
    public interface ICustomer : IAggregateRoot
    {
        Guid Id { get; }

        AccountCollection Accounts { get; }

        void Register(IAccount account);
    }
}