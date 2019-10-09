using System;
using Sample4.Domain.Accounts;
using Sample4.Domain.ValueObjects;

namespace Sample4.Domain.Customers
{
    public class Customer : ICustomer
    {
        public Guid Id { get; protected set; }

        public Name? Name { get; protected set; }

        public SSN? SSN { get; protected set; }
        
        public AccountCollection Accounts { get; protected set; }

        public Customer()
        {
            Accounts = new AccountCollection();
        }
        
        public void Register(IAccount account)
        {
            Accounts ??= new AccountCollection();
            Accounts.Add(account.Id);
        }
    }
}