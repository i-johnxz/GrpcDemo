using System.Collections.Generic;
using Sample3.API.Application.Commands;
using Sample3.API.Application.Models;


namespace Sample3.API.Extensions
{
    public static class BasketItemExtensions
    {
        public static IEnumerable<CreateOrderCommand.OrderItemDTO> ToOrderItemDtos(
            this IEnumerable<BasketItem> basketItems)
        {
            foreach (var item in basketItems)
            {
                yield return item.ToOrderItemDTO();
            }
        }

        public static CreateOrderCommand.OrderItemDTO ToOrderItemDTO(this BasketItem item)
        {
            return new CreateOrderCommand.OrderItemDTO()
            {
                ProductId = int.TryParse(item.ProductId, out int id) ? id : -1,
                ProductName = item.ProductName,
                PictureUrl = item.PictureUrl,
                UnitPrice = item.UnitPrice,
                Units = item.Quantity
            };
        }
    }
}