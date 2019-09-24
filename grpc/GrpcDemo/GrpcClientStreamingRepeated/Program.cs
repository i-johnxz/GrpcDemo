using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcServerStreamingRepeated.Protos;

namespace GrpcClientStreamingRepeated
{
    class Program
    {
        private static List<string> Fortunes { get; set; } = new List<string>()
        {
            "The fortune you seek is in another cookie.",
            "A closed mouth gathers no feet.",
            "A conclusion is simply the place where you got tired of thinking.",
            "A cynic is only a frustrated optimist.",
            "A foolish man listens to his heart. A wise man listens to cookies.",
            "You will die alone and poorly dressed.",
            "A fanatic is one who can't change his mind, and won't change the subject.",
            "If you look back, you'll soon be going that way.",
            "You will live long enough to open many fortune cookies.",
            "An alien of some sort will be appearing to you shortly."
        };

        static async Task Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Board.BoardClient(channel);

            using var tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            using var stream = client.ShowMessage(cancellationToken: token);

            foreach (var f in Fortunes)
            {
                if (token.IsCancellationRequested)
                    break;

                await stream.RequestStream.WriteAsync(new MessageRequest
                {
                    FortuneCookie = f
                });

                Console.WriteLine($"Sending \"{f}\" \n");
                await Task.Delay(1000, token);
            }

            await stream.RequestStream.CompleteAsync();

            var response = await stream.ResponseAsync;

            Console.WriteLine($"\n\n");

            foreach (var r in response.Fortunes)
            {
                Console.WriteLine(
                    $"Reply \"{r.Message}\". Original cookied received on {new DateTime(r.ReceivedTime)}. \n");
            }

            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
