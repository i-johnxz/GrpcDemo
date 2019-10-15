using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Grpc.Domain;

namespace ProductService.Grpc.DataAccess.EF.Configuration
{
    public class CoverConfig : IEntityTypeConfiguration<Cover>
    {
        public void Configure(EntityTypeBuilder<Cover> builder)
        {
            builder.HasOne(p => p.Product).WithMany(c => c.Covers);
            builder.Property(c => c.Code).IsRequired();
            builder.Property(c => c.Name).IsRequired();
        }
    }
}