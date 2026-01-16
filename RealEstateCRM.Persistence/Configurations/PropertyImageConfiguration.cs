using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateCRM.Domain.Entities;

namespace RealEstateCRM.Persistence.Configurations
{
    public class PropertyImageConfiguration : IEntityTypeConfiguration<PropertyImage>
    {
        public void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            builder.ToTable("PropertyImages");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(i => i.DisplayOrder)
                .HasDefaultValue(0);

            builder.Property(i => i.IsMainImage)
                .HasDefaultValue(false);

            builder.Property(i => i.UploadedAt)
                .IsRequired();

            builder.Property(i => i.CreatedAt)
                .IsRequired();

            builder.HasOne(i => i.Property)
                .WithMany(p => p.Images)
                .HasForeignKey(i => i.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}