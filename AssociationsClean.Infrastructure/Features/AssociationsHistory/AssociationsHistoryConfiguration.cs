using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AssociationsClean.Domain.Features.Associations;

namespace AssociationsClean.Infrastructure.Features.Associations
{
    internal sealed class AssociationsHistoryConfiguration : IEntityTypeConfiguration<AssociationHistory>
    {
        public void Configure(EntityTypeBuilder<AssociationHistory> builder)
        {
            builder.ToTable("AssociationsHistory");

            builder.HasKey(ah => new { ah.UserUuid, ah.AssociationId });

            builder.Property(ah => ah.UserUuid)
                .IsRequired();

            builder.Property(ah => ah.AssociationId)
                .IsRequired();

            builder.Property(ah => ah.AnsweredAt)
                .IsRequired();

            builder.HasOne<Association>() 
                .WithMany()
                .HasForeignKey(ah => ah.AssociationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(ah => new { ah.UserUuid, ah.AssociationId }).IsUnique();
        }
    }
}
