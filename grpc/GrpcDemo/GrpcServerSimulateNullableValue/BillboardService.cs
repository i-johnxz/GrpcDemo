using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcServerSimulateNullableValue.Protos;

namespace GrpcServerSimulateNullableValue
{
    public class BillboardService : Board.BoardBase
    {
        public override Task<MessageReply> ShowMessage(MessageRequest request, ServerCallContext context)
        {
            var message = new StringBuilder();
            message.AppendLine($"WageCase {request.WageCase}");
            message.AppendLine($"WageValue {request.WageValue}");
            message.AppendLine($"BonusCase {request.BonusCase}");
            if (request.BonusCase == MessageRequest.BonusOneofCase.None)
            {
                message.AppendLine(
                    "BonusCase None means that BonusValue property is never set by the requester. So you can ignore whatever values you see at BonusValue property.");
            }

            message.AppendLine($"BonusValue {request.BonusValue}");

            var now = DateTime.UtcNow;
            return Task.FromResult(new MessageReply
            {
                ReceivedTime = now.Ticks,
                Message = message.ToString()
            });
        }
    }
}
