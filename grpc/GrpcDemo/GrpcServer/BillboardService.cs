using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcServer.Protos;

namespace GrpcServer
{
    public class BillboardService : Board.BoardBase
    {
        public override Task<MessageReply> ShowMessage(MessageRequest request, ServerCallContext context)
        {
            var now = DateTime.UtcNow;
            return Task.FromResult(new MessageReply
            {
                DisplayTime = now.Ticks,
                ReceiveFrom = request.Sender
            });
        }
    }
}
