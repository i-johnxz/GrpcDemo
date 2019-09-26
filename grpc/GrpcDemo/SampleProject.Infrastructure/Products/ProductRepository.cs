﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.Products;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.Infrastructure.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly OrdersContext _context;

        public ProductRepository(OrdersContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetByIdsAsync(List<ProductId> ids)
        {
            return await this._context
                .Products.IncludePaths("_prices")
                .Where(x => ids.Contains(x.Id)).AsNoTracking().ToListAsync();
        }
    }
}