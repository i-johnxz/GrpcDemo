using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    public class DomainEventsDispatcherNotificationHandlerDecorator<T> : INotificationHandler<T> where T: INotification
    {

        private readonly INotificationHandler<T> _decorated;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public DomainEventsDispatcherNotificationHandlerDecorator(INotificationHandler<T> decorated, IDomainEventsDispatcher domainEventsDispatcher)
        {
            _decorated = decorated;
            _domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task Handle(T notification, CancellationToken cancellationToken)
        {
            await this._decorated.Handle(notification, cancellationToken);

            await this._domainEventsDispatcher.DispatchEventsAsync();
        }
    }
}
