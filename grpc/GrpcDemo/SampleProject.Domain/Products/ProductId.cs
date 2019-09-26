using System;
using System.Collections.Generic;
using System.Text;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Products
{
    public class ProductId : TypedIdValueBase
    {
        public ProductId(Guid value) : base(value)
        {
        }
    }
}
