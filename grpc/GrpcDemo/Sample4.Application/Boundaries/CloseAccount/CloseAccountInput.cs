using System;
using Sample4.Application.Exceptions;

namespace Sample4.Application.CloseAccount
{
    public sealed class CloseAccountInput
    {
        public Guid AccountId { get; }

        public CloseAccountInput(Guid accountId)
        {
            if (accountId == Guid.Empty)
            {
                throw new InputValidationException($"{nameof(accountId)} cannot be empty ");
            }

            AccountId = accountId;
        }
    }
}