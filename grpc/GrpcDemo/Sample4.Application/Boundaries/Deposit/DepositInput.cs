using System;
using Sample4.Application.Exceptions;
using Sample4.Domain.ValueObjects;

namespace Sample4.Application.Boundaries.Deposit
{
    public sealed class DepositInput
    {
        public Guid AccountId { get; }

        public PositiveMoney Amount { get; }

        public DepositInput(Guid accountId, PositiveMoney amount)
        {
            if (accountId == Guid.Empty)
            {
                throw new InputValidationException($"{nameof(accountId)} cannot be empty.");
            }

            AccountId = accountId;
            Amount = amount;
        }
    }
}