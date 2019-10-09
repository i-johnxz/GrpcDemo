using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Domain;

namespace Sample4.Infrastructure.EntityFrameworkDataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private MangaContext context;

        public UnitOfWork(MangaContext context)
        {
            this.context = context;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            int affectedRows = await context.SaveChangesAsync(cancellationToken);
            return affectedRows;
        }

        private bool disposed = false;

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}