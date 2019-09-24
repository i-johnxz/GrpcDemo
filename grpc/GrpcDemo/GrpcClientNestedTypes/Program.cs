using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcServerNestedTypes.Protos;

namespace GrpcClientNestedTypes
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //We need this switch because we are connecting to an unsecure server. If the server runs on SSL, there's no need for this switch.
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Board.BoardClient(channel);
            var request = new MessageRequest();
            request.Capabilities.Add(new MessageRequest.Types.SuperPower {Name = "Flying", Level = 1});
            request.Capabilities.Add(new MessageRequest.Types.SuperPower {Name = "Invisibility", Level = 10});
            request.Capabilities.Add(new MessageRequest.Types.SuperPower {Name = "Speed", Level = 5});

            var reply = await client.ShowMessageAsync(request);

            var displayDate = new DateTime(reply.ReceivedTime);
            Console.WriteLine(
                $"We sent a message to a gRPC server and  received  the following reply \n'\n{reply.Message}' \non {displayDate} ");
            Console.WriteLine("End");
            Console.Read();
        }
    }
}
