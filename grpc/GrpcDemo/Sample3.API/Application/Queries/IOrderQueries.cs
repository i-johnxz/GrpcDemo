using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sample3.Domain.AggregatesModel.OrderAggregate;

namespace Sample3.API.Application.Queries
{
    public interface IOrderQueries
    {
        Task<Order> GetOrderAsync(int id);

        Task<IEnumerable<OrderSummary>> GetOrdersFromUserAsync(Guid userId);


        Task<IEnumerable<CardType>> GetCardTypesAsync();
    }
}