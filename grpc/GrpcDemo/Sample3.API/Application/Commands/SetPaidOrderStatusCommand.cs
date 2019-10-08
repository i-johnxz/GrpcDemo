using System.Runtime.Serialization;
using MediatR;

namespace Sample3.API.Application.Commands
{
    public class SetPaidOrderStatusCommand : IRequest<bool>
    {
        [DataMember]
        public int OrderNumber { get; set; }

        public SetPaidOrderStatusCommand(int orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}