using System;
using BuildingBlocks.Domain;
using Sample4.Domain.ValueObjects;

namespace Sample4.Domain.Accounts
{
    public interface IAccount : IAggregateRoot
    {
        Guid Id { get; }
        ICredit Deposit(IEntityFactory entityFactory, PositiveMoney amountToDeposit);
        IDebit Withdraw(IEntityFactory entityFactory, PositiveMoney amountToWithdraw);
        bool IsClosingAllowed();
        Money GetCurrentBalance();
    }
}