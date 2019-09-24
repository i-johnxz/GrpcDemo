using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcServerMutipleType.Protos;

namespace GrpcServerMutipleType
{
    public class BillboardService : Board.BoardBase
    {
        public override Task<MessageReply> ShowMessage(MessageRequest request, ServerCallContext context)
        {
            var message = $"Your {request.Type} with the following content `{request.Message}` has arrived well.";
            var now = DateTime.UtcNow;
            return Task.FromResult(new MessageReply
            {
                ReceivedTime = now.Ticks,
                Message = message
            });
        }
    }
}
