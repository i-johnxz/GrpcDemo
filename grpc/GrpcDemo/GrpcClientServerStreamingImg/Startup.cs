using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServerServerStreamingImg.Protos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GrpcClientServerStreamingImg
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseStaticFiles();

            app.Run(async context =>
                {
                    AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

                    var channel = GrpcChannel.ForAddress("http://localhost:5000");
                    var client = new Board.BoardClient(channel);

                    var result = client.ShowMessage(new Empty());

                    context.Response.Headers["Content-Type"] = "text/html";

                    await context.Response.WriteAsync("<html><body><h1>Kitty Streaming</h1>");

                    using var tokenSource = new CancellationTokenSource();

                    CancellationToken token = tokenSource.Token;

                    var streamReader = result.ResponseStream;

                    //get metdata
                    string path = null;
                    await streamReader.MoveNext(token);

                    var initial = streamReader.Current;

                    if (initial.ImageCase == MessageReply.ImageOneofCase.MetaData)
                    {
                        path = Path.Combine(env.WebRootPath, initial.MetaData.FileName);
                    }

                    if (path == null)
                        throw new ApplicationException("Metadata is missing from the server");

                    using FileStream file = new FileStream(path, FileMode.Create);

                    int position = 0;
                    await foreach (var data in streamReader.ReadAllAsync(token))
                    {
                        var chunk = data.Chunk;
                        await file.WriteAsync(chunk.Data.ToByteArray(), 0, chunk.Length, token);
                        position += chunk.Length;
                        await context.Response.WriteAsync(position + " ", cancellationToken: token);
                    }

                    file.Close();



                    await context.Response.WriteAsync($"<img src=\"{initial.MetaData.FileName}\"/>", cancellationToken: token);

                    await context.Response.WriteAsync("</body></html>", cancellationToken: token);

                });

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
