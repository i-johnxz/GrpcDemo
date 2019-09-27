using System;
using MediatR;

namespace SampleProject.API.Orders.RemoveCustomerOrder
{
    public class RemoveCustomerOrderCommand : IRequest
    {
        public Guid CustomerId { get; set; }

        public Guid OrderId { get; set; }

        public RemoveCustomerOrderCommand(Guid customerId, Guid orderId)
        {
            CustomerId = customerId;
            OrderId = orderId;
        }
        
    }
}