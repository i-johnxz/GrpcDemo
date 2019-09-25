using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcServerDictionary.Protos;

namespace GrpcClientDictionary
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Board.BoardClient(channel);
            var request = new MessageRequest();
            request.Capabilities.Add("fly", new MessageRequest.Types.SuperPower { Name = "Flying", Level = 1 });
            request.Capabilities.Add("inv", new MessageRequest.Types.SuperPower { Name = "Invisibility", Level = 10 });
            request.Capabilities.Add("spe", new MessageRequest.Types.SuperPower { Name = "Speed", Level = 5 });

            var reply = await client.ShowMessageAsync(request);

            var displayDate = new DateTime(reply.ReceivedTime);
            Console.WriteLine(
                $"We sent a message to a gRPC server and  received  the following reply \n'\n{reply.Message}' \non {displayDate} ");
            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
