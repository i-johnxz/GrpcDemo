﻿namespace BuildingBlocks.Infrastructure.Outbox
{
    public interface IOutbox
    {
        void Add(OutboxMessage message);
    }
}
