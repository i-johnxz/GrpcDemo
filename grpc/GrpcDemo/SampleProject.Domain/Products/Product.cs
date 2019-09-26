﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SampleProject.Domain.SeedWork;
using SampleProject.Domain.SharedKernel;

namespace SampleProject.Domain.Products
{
    public class Product : Entity, IAggregateRoot
    {
        public ProductId Id { get; set; }

        public string Name { get; set; }

        private List<ProductPrice> _prices;

        private Product()
        {
            
        }

        internal MoneyValue GetPrice(string currency)
        {
            return this._prices.Single(x => x.Value.Currency == currency).Value;
        }
    }
}
