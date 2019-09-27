using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SampleProject.API.Orders.PlaceCustomerOrder
{
    public class OrderPlacedNotificationHandler : INotificationHandler<OrderPlacedNotification>
    {
        public Task Handle(OrderPlacedNotification notification, CancellationToken cancellationToken)
        {
            // Send email
            return Task.CompletedTask;
        }
    }
}