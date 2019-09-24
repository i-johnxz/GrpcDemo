using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer.Protos;

namespace GrpcClientStreaming
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //We need this switch because we are connecting to an unsecure server. If the server runs on SSL, there's no need for this switch.
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Board.BoardClient(channel);
            var result = client.ShowMessage(new MessageRequest
            {
                Name = "Johny"
            });

            using var tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            var streamReader = result.ResponseStream;

            await foreach (var reply in streamReader.ReadAllAsync(token))
            {
                var displayDate = new DateTime(reply.DisplayTime);
                Console.WriteLine($"Received \"{reply.Message}\" on {displayDate.ToLongTimeString()} \n");
            }

            Console.ReadLine();
        }
    }
}
