using System;
using System.Collections.Generic;
using Sample4.Domain.Customers;

namespace Sample4.Application.Boundaries.GetCustomerDetails
{
    public sealed class GetCustomerDetailsOutput
    {
        public Guid CustomerId { get; }

        public string SSN { get; }

        public string Name { get; }

        public IReadOnlyList<Account> Accounts { get; }

        public GetCustomerDetailsOutput(
            ICustomer customer,
            List<Account> accounts)
        {
            Customer customerEntity = (Customer) customer;
            
        }
    }
}