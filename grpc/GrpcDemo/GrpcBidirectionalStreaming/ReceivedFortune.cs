using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcBidirectionalStreaming
{
    public class ReceivedFortune
    {
        public string Message { get; set; }

        public DateTimeOffset Received { get; set; } = DateTimeOffset.UtcNow;
    }
}
