﻿using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
