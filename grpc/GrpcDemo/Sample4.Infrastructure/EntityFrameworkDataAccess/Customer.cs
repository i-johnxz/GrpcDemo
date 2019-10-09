using System;
using System.Collections.Generic;
using Sample4.Domain.Customers;
using Sample4.Domain.ValueObjects;

namespace Sample4.Infrastructure.EntityFrameworkDataAccess
{
    public class Customer : Domain.Customers.Customer
    {
        protected Customer()
        {
            
        }

        public Customer(SSN ssn, Name name)
        {
            Id = Guid.NewGuid();
            SSN = ssn;
            Name = name;
        }

        public void LoadAccounts(IEnumerable<Guid> accounts)
        {
            Accounts = new AccountCollection();
            Accounts.Add(accounts);
        }
    }
}