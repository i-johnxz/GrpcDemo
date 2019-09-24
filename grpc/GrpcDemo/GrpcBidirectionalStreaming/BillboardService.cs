using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcBidirectionalStreaming.Protos;

namespace GrpcBidirectionalStreaming
{
    public class BillboardService : Board.BoardBase
    {
        public override async Task ShowMessage(IAsyncStreamReader<MessageRequest> requestStream, IServerStreamWriter<MessageReply> responseStream, ServerCallContext context)
        {
            using var tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            await foreach (var request in requestStream.ReadAllAsync(cancellationToken: token))
            {
                await responseStream.WriteAsync(new MessageReply {Pong = "pong"});
                await Task.Delay(request.DelayTime, token);
            }
        }
    }
}
