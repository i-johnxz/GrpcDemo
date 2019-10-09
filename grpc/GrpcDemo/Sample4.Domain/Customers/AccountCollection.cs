using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sample4.Domain.Customers
{
    public sealed class AccountCollection
    {
        private readonly IList<Guid> _accountIds;

        public AccountCollection()
        {
            _accountIds = new List<Guid>();
        }

        public void Add(IEnumerable<Guid> accounts)
        {
            foreach (var account in accounts)
            {
                Add(account);
            }
        }

        public void Add(Guid accountId) => _accountIds.Add(accountId);

        public IReadOnlyCollection<Guid> GetAccountIds()
        {
            IReadOnlyCollection<Guid> accountIds = new ReadOnlyCollection<Guid>(_accountIds);
            return accountIds;
        }
    }
}