using System;
using Sample4.Application.Exceptions;

namespace Sample4.Application.Boundaries.GetCustomerDetails
{
    public sealed class GetCustomerDetailsInput
    {
        public Guid CustomerId { get; }

        public GetCustomerDetailsInput(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new InputValidationException($"{nameof(customerId)} cannot be empty");
            }

            CustomerId = customerId;
        }
    }
}