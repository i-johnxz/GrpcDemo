using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using SampleProject.Grpc.Api.Protos;

namespace SampleProject.Grpc.Api
{
    public class CustomerOrdersService : Protos.CustomerOrdersService.CustomerOrdersServiceBase
    {
        public override Task<OrderDtos> GetCustomerOrders(GetCusomerOrderRequest request, ServerCallContext context)
        {
            return base.GetCustomerOrders(request, context);
        }

        public override Task<OrderDetailsDto> GetCustomerOrderDetails(GetCustomerOrderDetailsRequest request, ServerCallContext context)
        {
            return base.GetCustomerOrderDetails(request, context);
        }

        public override Task<AddCustomerOrderResult> AddCustomerOrder(AddCustomerOrderRequest request, ServerCallContext context)
        {
            return base.AddCustomerOrder(request, context);
        }

        public override Task<ChangeCustomerOrderResult> ChangeCustomerOrder(ChangeCustomerOrderRequest request, ServerCallContext context)
        {
            return base.ChangeCustomerOrder(request, context);
        }

        public override Task<RemoveCustomerOrderResult> RemoveCustomerOrder(RemoveCustomerOrderRequest request, ServerCallContext context)
        {
            return base.RemoveCustomerOrder(request, context);
        }
    }

}
