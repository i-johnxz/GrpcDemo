using System;
using System.Collections.Generic;
using MediatR;

namespace SampleProject.API.Orders.ChangeCustomerOrder
{
    public class ChangeCustomerOrderCommand : IRequest
    {
        public Guid CustomerId { get; }

        public Guid OrderId { get; }

        public List<ProductDto> Products { get; }

        public ChangeCustomerOrderCommand(
            Guid customerId,
            Guid orderId,
            List<ProductDto> products)
        {
            CustomerId = customerId;
            OrderId = orderId;
            Products = products;
        }
    }
}