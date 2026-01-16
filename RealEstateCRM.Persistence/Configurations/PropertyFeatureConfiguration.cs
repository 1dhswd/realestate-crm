using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateCRM.Domain.Entities;

namespace RealEstateCRM.Persistence.Configurations
{
    public class PropertyFeatureConfiguration : IEntityTypeConfiguration<PropertyFeature>
    {
        public void Configure(EntityTypeBuilder<PropertyFeature> builder)
        {
            builder.ToTable("PropertyFeatures");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(f => f.Name)
                .IsUnique();

            builder.Property(f => f.Icon)
                .HasMaxLength(50);

            builder.Property(f => f.IsActive)
                .HasDefaultValue(true);

            builder.Property(f => f.CreatedAt)
                .IsRequired();
        }
    }
}