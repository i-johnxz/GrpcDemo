using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcBidirectionalStreaming.Protos;

namespace GrpcClientBidirectionalStreaming
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Board.BoardClient(channel);

            using var tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            using var stream = client.ShowMessage(cancellationToken: token);

            bool response = true;
            int inc = 1;

            do
            {
                try
                {
                    var delay = checked(1000 * inc);

                    await stream.RequestStream.WriteAsync(new MessageRequest
                    {
                        Ping = "Ping",
                        DelayTime = delay
                    });

                    inc++;
                    Console.WriteLine($"Send ping on {DateTimeOffset.UtcNow} \n");

                    response = await stream.ResponseStream.MoveNext(token);

                    if (response)
                    {
                        var result = stream.ResponseStream.Current;
                        Console.WriteLine($"Receive {result.Pong} on {DateTimeOffset.UtcNow} \n\n");
                    }

                }
                catch (OverflowException)
                {
                    inc = 1;
                }
            } while (response);


            Console.WriteLine("End");
            Console.ReadLine();

        }
    }
}
