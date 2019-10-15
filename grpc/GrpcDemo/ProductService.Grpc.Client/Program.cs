using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Grpc.Net.Client;
using ProductService.Grpc.Protos;

namespace ProductService.Grpc.Client
{
    class Program
    {
        static void Main(string[] args)
        {

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress("http://localhost:5000");

            var client = new Protos.ProductService.ProductServiceClient(channel);

            var result = client.GetAll(new FindAllProductsQuery());
            foreach (var resultProduct in result.Products)
            {
                Console.WriteLine(JsonSerializer.Serialize(resultProduct));
            }

            Console.WriteLine("End");
            Console.Read();
        }
    }
}
