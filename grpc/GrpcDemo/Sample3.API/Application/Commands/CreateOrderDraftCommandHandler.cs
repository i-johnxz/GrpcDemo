using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sample3.API.Extensions;
using Sample3.API.Infrastructure.Services;
using Sample3.Domain.AggregatesModel.OrderAggregate;

namespace Sample3.API.Application.Commands
{
    public class CreateOrderDraftCommandHandler
        : IRequestHandler<CreateOrderDraftCommand, OrderDraftDTO>
    {
        private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;

        public CreateOrderDraftCommandHandler(IIdentityService identityService, IMediator mediator)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public Task<OrderDraftDTO> Handle(CreateOrderDraftCommand message, CancellationToken cancellationToken)
        {
            var order = Order.NewDraft();
            var orderItems = message.Items.Select(i => i.ToOrderItemDTO());
            foreach (var item in orderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Discount, item.PictureUrl,
                    item.Units);
            }

            return Task.FromResult(OrderDraftDTO.FromOrder(order));
        }
    }

    public class OrderDraftDTO
    {
        public IEnumerable<CreateOrderCommand.OrderItemDTO> OrderItems { get; set; }

        public decimal Total { get; set; }

        public static OrderDraftDTO FromOrder(Order order)
        {
            return new OrderDraftDTO()
            {
                OrderItems = order.OrderItems.Select(oi => new CreateOrderCommand.OrderItemDTO
                {
                    Discount = oi.GetCurrentDiscount(),
                    ProductId = oi.ProductId,
                    UnitPrice = oi.GetUnitPrice(),
                    PictureUrl = oi.GetPictureUri(),
                    Units = oi.GetUnits(),
                    
                })
            };
        }
    } 
}