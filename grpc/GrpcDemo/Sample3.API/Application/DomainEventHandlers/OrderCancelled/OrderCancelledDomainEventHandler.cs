using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Sample3.API.Application.IntegrationEvents;
using Sample3.API.Application.IntegrationEvents.Events;
using Sample3.Domain.AggregatesModel.BuyerAggregate;
using Sample3.Domain.AggregatesModel.OrderAggregate;
using Sample3.Domain.Events;

namespace Sample3.API.Application.DomainEventHandlers.OrderCancelled
{
    public class OrderCancelledDomainEventHandler : INotificationHandler<OrderCancelledDomainEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBuyerRepository _buyerRepository;
        private readonly ILoggerFactory _logger;
        private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;

        public OrderCancelledDomainEventHandler(
            IOrderRepository orderRepository, 
            IBuyerRepository buyerRepository, 
            ILoggerFactory logger, 
            IOrderingIntegrationEventService orderingIntegrationEventService)
        {
            _orderRepository = orderRepository;
            _buyerRepository = buyerRepository;
            _logger = logger;
            _orderingIntegrationEventService = orderingIntegrationEventService;
        }


        public async Task Handle(OrderCancelledDomainEvent orderCancelledDomainEvent, CancellationToken cancellationToken)
        {
            _logger.CreateLogger<OrderCancelledDomainEvent>()
                .LogTrace("Order with Id: {OrderId} has been successfully updated to status {Status} ({Id})",
                    orderCancelledDomainEvent.Order.Id, nameof(OrderStatus.Cancelled), OrderStatus.Cancelled.Id);

            var order = await _orderRepository.GetAsync(orderCancelledDomainEvent.Order.Id);
            var buyer = await _buyerRepository.FindByIdAsync(order.GetBuyerId?.ToString());

            var orderStatusChangedToCancelledIntegrationEvent = new OrderStatusChangedToCancelledIntegrationEvent(
                order.Id,
                order.OrderStatus.Name, buyer.Name);

            await _orderingIntegrationEventService.AddAndSaveEventAsync(orderStatusChangedToCancelledIntegrationEvent);
        }
    }
}