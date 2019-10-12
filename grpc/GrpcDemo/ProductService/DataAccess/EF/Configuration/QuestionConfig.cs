using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Domain;

namespace ProductService.DataAccess.EF.Configuration
{
    public class QuestionConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasOne(p => p.Product).WithMany(p => p.Questions);
            builder.Property(q => q.Code).IsRequired();
            builder.Property(q => q.Index).IsRequired();

            builder.HasDiscriminator<int>("QuestionType")
                .HasValue<Question>(1)
                .HasValue<NumericQuestion>(2)
                .HasValue<DateQuestion>(3)
                .HasValue<ChoiceQuestion>(4);
            
            
        }
    }
}