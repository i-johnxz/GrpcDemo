using System;
using Sample4.Domain.ValueObjects;

namespace Sample4.Domain.Accounts
{
    public class Debit : IDebit
    {
        public Guid Id { get; protected set; }
        
        public PositiveMoney Amount { get; protected set; }

        public string Description
        {
            get => "Debit";
        }

        public DateTime TransactionDate { get; protected set; }

        public PositiveMoney Sum(PositiveMoney amount) => Amount.Add(amount);
    }
}