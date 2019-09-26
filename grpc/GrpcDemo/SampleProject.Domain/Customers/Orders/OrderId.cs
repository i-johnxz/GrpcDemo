using System;
using System.Collections.Generic;
using System.Text;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders
{
    public class OrderId : TypedIdValueBase
    {
        public OrderId(Guid value) : base(value)
        {
        }
    }
}
