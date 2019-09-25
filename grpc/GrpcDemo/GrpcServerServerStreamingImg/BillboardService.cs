using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServerServerStreamingImg.Protos;
using Microsoft.Extensions.Hosting;

namespace GrpcServerServerStreamingImg
{
    public class BillboardService : Board.BoardBase
    {
        private IHostEnvironment _env;

        public BillboardService(IHostEnvironment env)
        {
            _env = env;
        }

        public override async Task ShowMessage(Empty request, IServerStreamWriter<MessageReply> responseStream, ServerCallContext context)
        {
            using var tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            var meta = new MessageReply();
            var metaData = new ImageMetaData();
            metaData.FileName = "kitty.jpg";
            metaData.MimeType = "image/jpeg";
            meta.MetaData = metaData;

            await responseStream.WriteAsync(meta);

            var kitty = Path.Combine(_env.ContentRootPath, "kitty.jpg");

            using var reader = new FileStream(kitty, FileMode.Open);

            int chunkSize = 100;
            int bytesRead;
            byte[] buffer = new byte[chunkSize];
            int position = 0;
            long length = reader.Length;
            while ((bytesRead = await reader.ReadAsync(buffer, 0, buffer.Length, token)) > 0)
            {
                var reply = new MessageReply();
                var chunk = new ImageChunk();
                chunk.Length = bytesRead;
                chunk.Data = ByteString.CopyFrom(buffer);
                reply.Chunk = chunk;

                await responseStream.WriteAsync(reply);

                position += bytesRead;
                Console.WriteLine(position);
            }
            reader.Close();
            Console.WriteLine("Done");
        }
    }
}
