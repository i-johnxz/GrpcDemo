using System;
using System.Collections.Generic;
using Sample4.Domain.Accounts;
using Sample4.Domain.Customers;

namespace Sample4.Infrastructure.EntityFrameworkDataAccess
{
    public class Account : Domain.Accounts.Account
    {
        public Guid CustomerId { get; protected set; }

        protected Account()
        {
            
        }

        public Account(ICustomer customer)
        {
            Id = Guid.NewGuid();
            CustomerId = customer.Id;
        }

        public void Load(IList<Credit> credits, IList<Debit> debits)
        {
            Credits = new CreditsCollection();
            Credits.Add(credits);
            
            Debits = new DebitsCollection();
            Debits.Add(debits);
        }
    }
}