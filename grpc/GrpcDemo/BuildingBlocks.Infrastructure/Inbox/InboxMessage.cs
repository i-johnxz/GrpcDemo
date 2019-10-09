using System;

namespace BuildingBlocks.Infrastructure.Inbox
{
    public class InboxMessage
    {
        public Guid Id { get; set; }

        public DateTime OccurredOn { get; set; }

        public string Type { get; set; }

        public string Data { get; set; }

        public DateTime? ProcessedDate { get; set; }

        private InboxMessage()
        {
            
        }

        public InboxMessage(Guid id, DateTime occurredOn, string type, string data, DateTime? processedDate)
        {
            Id = id;
            OccurredOn = occurredOn;
            Type = type;
            Data = data;
        }
    }
}
