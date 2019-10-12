using System;
using Sample4.Domain.Accounts;

namespace Sample4.Application.CloseAccount
{
    public sealed class CloseAccountOutput
    {
        public Guid AccountId { get; }

        public CloseAccountOutput(IAccount account)
        {
            AccountId = account.Id;
        }
    }
}