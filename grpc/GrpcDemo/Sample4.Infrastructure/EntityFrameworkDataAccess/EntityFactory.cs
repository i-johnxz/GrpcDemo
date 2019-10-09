using System;
using Sample4.Domain;
using Sample4.Domain.Accounts;
using Sample4.Domain.Customers;
using Sample4.Domain.ValueObjects;

namespace Sample4.Infrastructure.EntityFrameworkDataAccess
{
    public sealed class EntityFactory : IEntityFactory
    {
        public ICustomer NewCustomer(SSN ssn, Name name)
            => new Customer(ssn, name);

        public IAccount NewAccount(ICustomer customer)
            => new Account(customer);

        public ICredit NewCredit(IAccount account, PositiveMoney amountToDeposit, DateTime transactionDate)
            => new Credit(account, amountToDeposit, transactionDate);

        public IDebit NewDebit(IAccount account, PositiveMoney amountToWithdraw, DateTime transactionDate)
            => new Debit(account, amountToWithdraw, transactionDate);
    }
}