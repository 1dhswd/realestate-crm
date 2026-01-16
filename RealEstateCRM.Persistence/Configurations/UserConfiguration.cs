using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateCRM.Domain.Entities;

namespace RealEstateCRM.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(u => u.IsActive)
                .HasDefaultValue(true);

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.HasMany(u => u.Properties)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.AssignedCustomers)
                .WithOne(c => c.AssignedAgent)
                .HasForeignKey(c => c.AssignedAgentId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.CreatedLeads)
                .WithOne(l => l.CreatedByUser)
                .HasForeignKey(l => l.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Appointments)
                .WithOne(a => a.Agent)
                .HasForeignKey(a => a.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.CreatedOffers)
                .WithOne(o => o.CreatedByUser)
                .HasForeignKey(o => o.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.ActivityLogs)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}