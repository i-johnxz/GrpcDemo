using System;
using System.Collections.Generic;
using MediatR;

namespace SampleProject.API.Orders.GetCustomerOrders
{
    internal class GetCustomerOrdersQuery : IRequest<List<OrderDto>>
    {
        public Guid CustomerId { get; set; }

        public GetCustomerOrdersQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}