using System;
using System.Collections.Generic;
using System.Text;

namespace SampleProject.Domain.SharedKernel
{
    public class MoneyValue
    {
        public decimal Value { get; set; }

        public string Currency { get; set; }

        public MoneyValue(decimal value, string currency)
        {
            Value = value;
            Currency = currency;
        }
    }
}
