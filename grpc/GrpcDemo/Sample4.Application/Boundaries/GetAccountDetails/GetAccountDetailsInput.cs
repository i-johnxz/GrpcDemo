using System;
using Sample4.Application.Exceptions;

namespace Sample4.Application.Boundaries.GetAccountDetails
{
    public class GetAccountDetailsInput
    {
        public Guid AccountId { get; }

        public GetAccountDetailsInput(Guid accountId)
        {
            if (accountId == Guid.Empty)
            {
                throw new InputValidationException($"{nameof(accountId)} cannot be empty");
            }

            AccountId = accountId;
        }
    }
}