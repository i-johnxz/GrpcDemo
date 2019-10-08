using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Sample3.Domain.AggregatesModel.OrderAggregate;
using Sample3.Infrastructure.Idempotency;

namespace Sample3.API.Application.Commands
{
    // Regular CommandHandler
    public class SetStockConfirmedOrderStatusCommandHandler : IRequestHandler<SetStockConfirmedOrderStatusCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public SetStockConfirmedOrderStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Handler which processes the command when
        /// Stock service confirms the request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> Handle(SetStockConfirmedOrderStatusCommand command, CancellationToken cancellationToken)
        {
            await Task.Delay(10000, cancellationToken);

            var orderToUpdate = await _orderRepository.GetAsync(command.OrderNumber);
            if (orderToUpdate == null)
            {
                return false;
            }

            orderToUpdate.SetStockConfirmedStatus();
            return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }

    public class
        SetStockConfirmedOrderStatusIdentifiedCommandHandler : IdentifiedCommandHandler<
            SetStockConfirmedOrderStatusCommand, bool>
    {
        public SetStockConfirmedOrderStatusIdentifiedCommandHandler(
            IMediator mediator, 
            IRequestManager requestManager,
            ILogger<IdentifiedCommandHandler<SetStockConfirmedOrderStatusCommand, bool>> logger) 
            : base(mediator, requestManager, logger)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;
        }
    }
}