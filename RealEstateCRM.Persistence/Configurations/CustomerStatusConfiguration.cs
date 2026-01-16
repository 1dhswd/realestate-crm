using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateCRM.Domain.Entities;

namespace RealEstateCRM.Persistence.Configurations
{
    public class CustomerStatusConfiguration : IEntityTypeConfiguration<CustomerStatus>
    {
        public void Configure(EntityTypeBuilder<CustomerStatus> builder)
        {
            builder.ToTable("CustomerStatuses");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(s => s.Name).IsUnique();

            builder.Property(s => s.ColorCode)
                .HasMaxLength(7);

            builder.Property(s => s.DisplayOrder)
                .HasDefaultValue(0);

            builder.Property(s => s.CreatedAt)
                .IsRequired();

            builder.HasMany(s => s.Customers)
                .WithOne(c => c.Status)
                .HasForeignKey(c => c.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
