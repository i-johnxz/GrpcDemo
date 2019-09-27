using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Domain.Customers
{
    public interface ICustomerUniquenessChecker
    {
        bool IsUnique(Customer customer);
    }
}
