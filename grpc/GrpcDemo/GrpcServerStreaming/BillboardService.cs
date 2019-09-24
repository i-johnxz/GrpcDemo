using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcServer.Protos;

namespace GrpcServerStreaming
{
    public class BillboardService : Board.BoardBase
    {
        public override async Task ShowMessage(MessageRequest request, IServerStreamWriter<MessageReply> responseStream, ServerCallContext context)
        {
            foreach (var x in Enumerable.Range(1, 10))
            {
                var now = DateTime.UtcNow;

                await responseStream.WriteAsync(new MessageReply
                {
                    DisplayTime = now.Ticks,
                    Message = $"Hello {request.Name}"
                });

                await Task.Delay(5000);
            }
        }
    }
}
