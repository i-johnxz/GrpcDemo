using Sample4.Domain.Accounts;
using Sample4.Domain.ValueObjects;

namespace Sample4.Application.Boundaries.Deposit
{
    public sealed class DepositOutput
    {
        public Transaction Transaction { get; }

        public decimal UpdatedBalance { get; }

        public DepositOutput(
            ICredit credit,
            Money updatedBalance)
        {
            Credit creditEntity = (Credit) credit;
            
            Transaction = new Transaction(
                creditEntity.Description,
                creditEntity
                    .Amount
                    .ToMoney()
                    .ToDecimal(),
                creditEntity.TransactionDate);

            UpdatedBalance = updatedBalance.ToDecimal();
        }
    }
}