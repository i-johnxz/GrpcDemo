using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using SampleProject.Grpc.Api.Protos;

namespace SampleProject.Grpc.Api
{
    public class CustomersService : CustomerService.CustomerServiceBase
    {
        

        public override Task<CustomerDto> RegisterCustomer(RegisterCustomerRequest request, ServerCallContext context)
        {
            return base.RegisterCustomer(request, context);
        }
    }
}
