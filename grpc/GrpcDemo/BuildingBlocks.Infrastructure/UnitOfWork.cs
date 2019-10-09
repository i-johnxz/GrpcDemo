using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.DomainEventsDispatching;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork(DbContext context, IDomainEventsDispatcher domainEventsDispatcher)
        {
            _context = context;
            _domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await this._domainEventsDispatcher.DispatchEventsAsync();
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
