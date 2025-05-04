using Microsoft.EntityFrameworkCore;
using AssociationsClean.Domain.Features.Categories;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssociationsClean.Infrastructure.Features.Categories
{
    internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.Photo).IsRequired(false);

            builder.HasMany(c => c.Associations)
                   .WithOne(a => a.Category)
                   .HasForeignKey(a => a.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
