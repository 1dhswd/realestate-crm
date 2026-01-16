using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateCRM.Domain.Entities;

namespace RealEstateCRM.Persistence.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("Properties");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasColumnType("nvarchar(max)");

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Area)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.Address)
                .HasMaxLength(500);

            builder.Property(p => p.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.District)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Latitude)
                .HasColumnType("decimal(10,8)");

            builder.Property(p => p.Longitude)
                .HasColumnType("decimal(11,8)");

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder.Property(p => p.IsFeatured)
                .HasDefaultValue(false);

            builder.Property(p => p.ViewCount)
                .HasDefaultValue(0);

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.HasIndex(p => p.City);
            builder.HasIndex(p => p.CategoryId);
            builder.HasIndex(p => p.IsActive);
            builder.HasIndex(p => p.Price);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Properties)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Type)
                .WithMany(t => t.Properties)
                .HasForeignKey(p => p.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Images)
                .WithOne(i => i.Property)
                .HasForeignKey(i => i.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Leads)
                .WithOne(l => l.Property)
                .HasForeignKey(l => l.PropertyId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.Appointments)
                .WithOne(a => a.Property)
                .HasForeignKey(a => a.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Offers)
                .WithOne(o => o.Property)
                .HasForeignKey(o => o.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}