using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Sample3.API.Application.IntegrationEvents;
using Sample3.API.Infrastructure.Services;
using Sample3.Domain.AggregatesModel.OrderAggregate;

namespace Sample3.API.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;
        private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;
        private readonly ILogger<CreateOrderCommandHandler> _logger;


        public CreateOrderCommandHandler(IOrderRepository orderRepository, 
            IIdentityService identityService, 
            IMediator mediator, 
            IOrderingIntegrationEventService orderingIntegrationEventService, 
            ILogger<CreateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(orderRepository));
            _orderingIntegrationEventService = orderingIntegrationEventService ??
                                               throw new ArgumentNullException(nameof(orderingIntegrationEventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<bool> Handle(CreateOrderCommand message, CancellationToken cancellationToken)
        {
            // Add Integration event to clean the basket
            return await Task.FromResult(true);
        }
    }
}