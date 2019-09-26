using System;
using System.Collections.Generic;
using System.Text;
using SampleProject.Domain.SharedKernel;

namespace SampleProject.Domain.Products
{
    public class ProductPrice
    {
        public MoneyValue Value { get; protected set; }

        private ProductPrice()
        {
            
        }
    }
}
