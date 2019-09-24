using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcServerMutipleType.Protos;

namespace GrpcClientMutipleType
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
                Message = "Hello World",
                Sender = "Dody Gunawinata",
                Type = MessageRequest.Types.MessageType.LongForm
            });

            var displayDate = new DateTime(reply.ReceivedTime);
            Console.WriteLine(
                $"We sent a message to a gRPC server and  received  the following reply '{reply.Message}' on {displayDate} ");

            Console.WriteLine("End");
            Console.Read();
        }
    }
}
