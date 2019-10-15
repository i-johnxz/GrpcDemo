using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Grpc.Domain;

namespace ProductService.Grpc.DataAccess.EF.Configuration
{
    public class ChoiceConfig : IEntityTypeConfiguration<Choice>
    {
        public void Configure(EntityTypeBuilder<Choice> builder)
        {
            builder.HasKey("Code");
            builder.HasOne(q => q.Question).WithMany(c => c.Choices);
        }
    }
}