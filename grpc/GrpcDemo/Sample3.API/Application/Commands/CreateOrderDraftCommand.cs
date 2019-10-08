using System.Collections.Generic;
using MediatR;
using Sample3.API.Application.Models;

namespace Sample3.API.Application.Commands
{
    public class CreateOrderDraftCommand : IRequest<OrderDraftDTO>
    {
        public string BuyerId { get; private set; }

        public IEnumerable<BasketItem> Items { get; private set; }

        public CreateOrderDraftCommand(string buyerId, IEnumerable<BasketItem> items)
        {
            BuyerId = buyerId;
            Items = items;
        }
    }
}