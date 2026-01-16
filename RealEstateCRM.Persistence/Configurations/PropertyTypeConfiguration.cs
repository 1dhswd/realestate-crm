using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateCRM.Domain.Entities;

namespace RealEstateCRM.Persistence.Configurations
{
    public class PropertyTypeConfiguration : IEntityTypeConfiguration<PropertyType>
    {
        public void Configure(EntityTypeBuilder<PropertyType> builder)
        {
            builder.ToTable("PropertyTypes");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(t => t.Name)
                .IsUnique();

            builder.Property(t => t.IsActive)
                .HasDefaultValue(true);
        }
    }
}