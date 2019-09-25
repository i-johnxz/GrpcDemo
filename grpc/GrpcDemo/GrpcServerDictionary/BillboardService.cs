using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcServerDictionary.Protos;

namespace GrpcServerDictionary
{
    public class BillboardService : Board.BoardBase
    {
        public override Task<MessageReply> ShowMessage(MessageRequest request, ServerCallContext context)
        {
            var message = new StringBuilder();
            foreach (var (k, c) in request.Capabilities)
            {
                message.AppendLine($"'{k}' = {c.Name} level {c.Level}");
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
