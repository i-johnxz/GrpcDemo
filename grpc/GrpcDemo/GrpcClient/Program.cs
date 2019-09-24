using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcServer.Protos;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //We need this switch because we are connecting to an unsecure server. If the server runs on SSL, there's no need for this switch.
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Board.BoardClient(channel);
            var reply = await client.ShowMessageAsync(new MessageRequest
            {
                Message = "Good morning people of the world",
                Sender = "Dody Gunawinata"
            });

            var displayDate = new DateTime(reply.DisplayTime);
            Console.WriteLine($"This server sends a gRPC request to a server and get the following result: Received message on {displayDate} from {reply.ReceiveFrom}");
            Console.ReadLine();
        }
    }
}
