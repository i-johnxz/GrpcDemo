using System.Threading.Tasks;
using ApplicationCore.Entities.OrderAggregate;

namespace ApplicationCore.Interfaces
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<Order> GetByIdWithItemsAsync(int id);
    }
}