using System.Runtime.Serialization;
using MediatR;

namespace Sample3.API.Application.Commands
{
    public class SetAwaitingValidationOrderStatusCommand : IRequest<bool>
    {
        [DataMember]
        public int OrderNumber { get; set; }

        public SetAwaitingValidationOrderStatusCommand(int orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}