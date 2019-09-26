using System;
using System.Collections.Generic;
using System.Text;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers
{
    public class Customer : Entity, IAggregateRoot
    {
        public CustomerId Id { get; protected set; }

        public string Email { get; protected set; }

        public string Name { get; protected set; }
        
    }
}
