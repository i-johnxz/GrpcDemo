using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleProject.Domain.Customers;

namespace SampleProject.Infrastructure.Customers
{
    internal class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        internal const string OrdersList = "_orders";
        internal const string OrderProducts = "_orderProducts";
        
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(nameof(Customers), Scheman)
        }
    }
}