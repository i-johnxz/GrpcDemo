using System;
using System.Collections.Generic;
using System.Text;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers
{
    public class CustomerRegisteredEvent : DomainEventBase
    {
        public Customer Customer { get; }


        public CustomerRegisteredEvent(Customer customer)
        {
            Customer = customer;
        }
    }
}
