using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.Customers;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.Infrastructure.Customers
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly OrdersContext _context;

        public CustomerRepository(OrdersContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<Customer> GetByIdAsync(CustomerId id)
        {
            return await this._context.Customers.IncludePaths(
                CustomerEntityTypeConfiguration.OrdersList,
                CustomerEntityTypeConfiguration.OrderProducts
            ).AsNoTracking().SingleAsync(x => x.Id == id);
        }

        public async Task AddAsync(Customer customer)
        {
            await this._context.Customers.AddAsync(customer);
        }
    }
}