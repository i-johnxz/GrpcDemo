using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Domain.Customers
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(CustomerId id);

        Task AddAsync(Customer customer);
    }
}
