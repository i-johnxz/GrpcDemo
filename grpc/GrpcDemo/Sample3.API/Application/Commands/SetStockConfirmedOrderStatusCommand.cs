using System.Runtime.Serialization;
using MediatR;

namespace Sample3.API.Application.Commands
{
    public class SetStockConfirmedOrderStatusCommand : IRequest<bool>
    {
        [DataMember]
        public int OrderNumber { get; private set; }

        public SetStockConfirmedOrderStatusCommand(int orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}