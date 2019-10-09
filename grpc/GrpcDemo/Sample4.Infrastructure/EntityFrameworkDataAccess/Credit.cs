using System;
using Sample4.Domain.Accounts;
using Sample4.Domain.ValueObjects;

namespace Sample4.Infrastructure.EntityFrameworkDataAccess
{
    public class Credit : Domain.Accounts.Credit
    {
        public Guid AccountId { get; protected set; }

        protected Credit()
        {
            
        }

        public Credit(IAccount account, PositiveMoney amountToDeposit, DateTime transactionDate)
        {
            this.AccountId = account.Id;
            this.Amount = amountToDeposit;
            this.TransactionDate = transactionDate;
        }
    }
}