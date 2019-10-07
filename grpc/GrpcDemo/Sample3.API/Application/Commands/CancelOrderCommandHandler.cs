using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Sample3.Domain.AggregatesModel.OrderAggregate;
using Sample3.Infrastructure.Idempotency;

namespace Sample3.API.Application.Commands
{
    // Regular CommandHandler
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public CancelOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Handler which processes the command when
        /// customer executes cancel order from app
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.GetAsync(command.OrderNumber);
            if (orderToUpdate == null)
            {
                return false;
            }
            
            orderToUpdate.SetCancelledStatus();
            return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class CancelOrderIdentifiedCommandHandler : IdentifiedCommandHandler<CancelOrderCommand, bool>
    {
        public CancelOrderIdentifiedCommandHandler(
            IMediator mediator,
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<CancelOrderCommand, bool>> logger)
            :base(mediator, requestManager, logger)
        {
            
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;
        }
    }
}