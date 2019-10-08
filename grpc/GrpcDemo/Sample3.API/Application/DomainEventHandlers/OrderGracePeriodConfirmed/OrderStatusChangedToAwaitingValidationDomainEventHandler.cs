using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Sample3.API.Application.IntegrationEvents;
using Sample3.API.Application.IntegrationEvents.Events;
using Sample3.Domain.AggregatesModel.BuyerAggregate;
using Sample3.Domain.AggregatesModel.OrderAggregate;
using Sample3.Domain.Events;

namespace Sample3.API.Application.DomainEventHandlers.OrderGracePeriodConfirmed
{
    public class OrderStatusChangedToAwaitingValidationDomainEventHandler
        : INotificationHandler<OrderStatusChangedToAwaitingValidationDomainEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILoggerFactory _logger;
        private readonly IBuyerRepository _buyerRepository;
        private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;


        public OrderStatusChangedToAwaitingValidationDomainEventHandler(
            IOrderRepository orderRepository, 
            ILoggerFactory logger, 
            IBuyerRepository buyerRepository, 
            IOrderingIntegrationEventService orderingIntegrationEventService)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _buyerRepository = buyerRepository;
            _orderingIntegrationEventService = orderingIntegrationEventService;  
        }

        public async Task Handle(OrderStatusChangedToAwaitingValidationDomainEvent orderStatusChangedToAwaitingValidationDomainEvent, CancellationToken cancellationToken)
        {
            _logger.CreateLogger<OrderStatusChangedToAwaitingValidationDomainEvent>()
                .LogTrace("Order with Id: {OrderId} has been successfully updated to status {Status} ({Id})",
                    orderStatusChangedToAwaitingValidationDomainEvent.OrderId, nameof(OrderStatus.AwaitingValidation), OrderStatus.AwaitingValidation.Id);

            var order = await _orderRepository.GetAsync(orderStatusChangedToAwaitingValidationDomainEvent.OrderId);

            var buyer = await _buyerRepository.FindByIdAsync(order.GetBuyerId?.ToString());

            var orderStockList = orderStatusChangedToAwaitingValidationDomainEvent.OrderItems
                .Select(orderItem => new OrderStockItem(orderItem.ProductId, orderItem.GetUnits()));

            var orderStatusChangedToAwaitingValidationIntegrationEvent =
                new OrderStatusChangedToAwaitingValidationIntegrationEvent(
                    order.Id, order.OrderStatus.Name, buyer.Name, orderStockList);

            await _orderingIntegrationEventService.AddAndSaveEventAsync(
                orderStatusChangedToAwaitingValidationIntegrationEvent);
        }
    }
}