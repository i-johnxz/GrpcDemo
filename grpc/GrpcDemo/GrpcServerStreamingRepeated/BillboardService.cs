using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcServerStreamingRepeated.Protos;

namespace GrpcServerStreamingRepeated
{
    public class BillboardService : Board.BoardBase
    {
        public override async Task<MessageReply> ShowMessage(IAsyncStreamReader<MessageRequest> requestStream, ServerCallContext context)
        {
            var fortunes = new List<ReceivedFortune>();

            using var tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            await foreach (var request in requestStream.ReadAllAsync(token))
            {
                var inBed = request.FortuneCookie[0..^1] + " in bed.";

                fortunes.Add(new ReceivedFortune {Message = inBed});
            }

            var reply = new MessageReply();

            foreach (var f in fortunes)
            {
                reply.Fortunes.Add(new TranslatedFortune
                {
                    Message = f.Message,
                    ReceivedTime = f.Received.Ticks
                });
            }

            return reply;
        }
    }
}
