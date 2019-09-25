using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcServerSimulateNullableValue.Protos;

namespace GrpcClientSimulateNullableValue
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress("http://localhost:5000");

            var client = new Board.BoardClient(channel);
            var request = new MessageRequest();
            request.WageValue = 10_000;
            request.BonusValue = 9_000;

            var reply = await client.ShowMessageAsync(request);

            var displayDate = new DateTime(reply.ReceivedTime);
            Console.WriteLine($"We sent a message to a gRPC server and  received  the following reply \n'\n{reply.Message}' \non {displayDate} ");
            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
