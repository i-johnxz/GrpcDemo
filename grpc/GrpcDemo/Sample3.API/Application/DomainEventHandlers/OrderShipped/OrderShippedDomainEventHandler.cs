using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Sample3.API.Application.IntegrationEvents;
using Sample3.API.Application.IntegrationEvents.Events;
using Sample3.Domain.AggregatesModel.BuyerAggregate;
using Sample3.Domain.AggregatesModel.OrderAggregate;
using Sample3.Domain.Events;

namespace Sample3.API.Application.DomainEventHandlers.OrderShipped
{
    public class OrderShippedDomainEventHandler : INotificationHandler<OrderShippedDomainEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBuyerRepository _buyerRepository;
        private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;
        private readonly ILoggerFactory _logger;

        public OrderShippedDomainEventHandler(
            IOrderRepository orderRepository, 
            IBuyerRepository buyerRepository, 
            IOrderingIntegrationEventService orderingIntegrationEventService, 
            ILoggerFactory logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _buyerRepository = buyerRepository ?? throw new ArgumentNullException(nameof(buyerRepository));
            _orderingIntegrationEventService = orderingIntegrationEventService ?? throw new ArgumentNullException(nameof(orderingIntegrationEventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task Handle(OrderShippedDomainEvent orderShippedDomainEvent, CancellationToken cancellationToken)
        {
            _logger.CreateLogger<OrderShippedDomainEvent>().
                LogTrace("Order with Id: {OrderId} has been successfully updated to status {Status} ({Id})",
                orderShippedDomainEvent.Order.Id, nameof(OrderStatus.Shipped), OrderStatus.Shipped.Id);

            var order = await _orderRepository.GetAsync(orderShippedDomainEvent.Order.Id);
            var buyer = await _buyerRepository.FindByIdAsync(order.GetBuyerId?.ToString());

            var orderStatusChangedToShippedIntegrationEvent = new OrderStatusChangedToShippedIntegrationEvent(
                order.Id,
                order.OrderStatus.Name,
                buyer.Name);
            await _orderingIntegrationEventService.AddAndSaveEventAsync(orderStatusChangedToShippedIntegrationEvent);
        }
    }
}