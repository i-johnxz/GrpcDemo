using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcServerNestedTypes.Protos;

namespace GrpcServerNestedTypes
{
    public class BillboardService : Board.BoardBase
    {
        public override Task<MessageReply> ShowMessage(MessageRequest request, ServerCallContext context)
        {
            var message = new StringBuilder();
            foreach (var c in request.Capabilities)
            {
                message.AppendLine($"{c.Name} level {c.Level}");
            }

            var now = DateTime.UtcNow;
            return Task.FromResult(new MessageReply
            {
                ReceivedTime = now.Ticks,
                Message = message.ToString()
            });

        }
    }
}
