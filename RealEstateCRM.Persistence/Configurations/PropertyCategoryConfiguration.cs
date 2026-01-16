using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateCRM.Domain.Entities;

namespace RealEstateCRM.Persistence.Configurations
{
    public class PropertyCategoryConfiguration : IEntityTypeConfiguration<PropertyCategory>
    {
        public void Configure(EntityTypeBuilder<PropertyCategory> builder)
        {
            builder.ToTable("PropertyCategories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(c => c.Name)
                .IsUnique();

            builder.Property(c => c.Description)
                .HasMaxLength(200);

            builder.Property(c => c.IsActive)
                .HasDefaultValue(true);
        }
    }
}