using System;
using Sample4.Domain.Accounts;
using Sample4.Domain.ValueObjects;

namespace Sample4.Infrastructure.EntityFrameworkDataAccess
{
    public class Debit : Domain.Accounts.Debit
    {
        public Guid AccountId { get; protected set; }

        protected Debit()
        {
            
        }

        public Debit(IAccount account, PositiveMoney amountToWithdraw, DateTime transactionDate)
        {
            this.AccountId = account.Id;
            this.Amount = amountToWithdraw;
            this.TransactionDate = transactionDate;
        }
    }
}