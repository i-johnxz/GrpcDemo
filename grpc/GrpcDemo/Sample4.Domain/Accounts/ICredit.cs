using System;
using Sample4.Domain.ValueObjects;

namespace Sample4.Domain.Accounts
{
    public interface ICredit
    {
        Guid Id { get; }

        PositiveMoney Sum(PositiveMoney amount);
    }
}