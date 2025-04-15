using AssociationsClean.Domain.Features.Associations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AssociationsClean.Infrastructure.Features.Associations
{
    internal sealed class AssociationConfiguration : IEntityTypeConfiguration<Association>
    {
        public void Configure(EntityTypeBuilder<Association> builder)
        {
            builder.ToTable("Associations");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.Description)
                .HasMaxLength(1000); 

            builder.Property(a => a.CategoryId)
                .IsRequired();

         
            builder.HasIndex(a => new { a.Name, a.CategoryId })
                .IsUnique();

          
            builder.HasOne(a => a.Category)
                .WithMany() 
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
