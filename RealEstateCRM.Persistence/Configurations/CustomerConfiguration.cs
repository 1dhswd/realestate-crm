using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateCRM.Domain.Entities;

namespace RealEstateCRM.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Email)
                .HasMaxLength(100);

            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(c => c.PhoneNumber);

            builder.Property(c => c.Address)
                .HasMaxLength(500);

            builder.Property(c => c.Notes)
                .HasColumnType("nvarchar(max)");

            builder.Property(c => c.Source)
                .HasMaxLength(50);

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.HasOne(c => c.AssignedAgent)
                .WithMany(u => u.AssignedCustomers)
                .HasForeignKey(c => c.AssignedAgentId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.Leads)
                .WithOne(l => l.Customer)
                .HasForeignKey(l => l.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}