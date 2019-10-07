using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Entities.BasketAggregate;
using ApplicationCore.Entities.OrderAggregate;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;

namespace ApplicationCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IAsyncRepository<Order> _orderRepository;
        private readonly IAsyncRepository<Basket> _basketRepository;
        private readonly IAsyncRepository<CatalogItem> _itemRepository;

        public OrderService(IAsyncRepository<Order> orderRepository, 
            IAsyncRepository<Basket> basketRepository, 
            IAsyncRepository<CatalogItem> itemRepository)
        {
            _orderRepository = orderRepository;
            _basketRepository = basketRepository;
            _itemRepository = itemRepository;
        }
        
        
        public async Task CreateOrderAsync(int basketId, Address shippingAddress)
        {
            var basket = await _basketRepository.GetByIdAsync(basketId);
            Guard.Against.NullBasket(basketId, basket);
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var catalogItem = await _itemRepository.GetByIdAsync(item.CatalogItemId);
                var itemOrdered = new CatalogItemOrdered(catalogItem.Id,
                    catalogItem.Name,
                    catalogItem.PictureUri);
                var orderItem = new OrderItem(itemOrdered, item.UnitPrice, item.Quantity);
                items.Add(orderItem);
            }
            var order = new Order(basket.BuyerId, shippingAddress, items);

            await _orderRepository.AddAsync(order);
        }
    }
}