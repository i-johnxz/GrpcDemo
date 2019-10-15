using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Grpc.Domain;

namespace ProductService.Grpc.DataAccess.EF.Configuration
{
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.Property(q => q.Code).IsRequired();
            builder.Property(q => q.Name).IsRequired();
        }
    }
}