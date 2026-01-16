using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateCRM.Domain.Entities;

namespace RealEstateCRM.Persistence.Configurations
{
    public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            builder.ToTable("ActivityLogs");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.EntityType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.EntityId)
                .IsRequired();

            builder.Property(a => a.Action)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Description)
                .HasMaxLength(500);

            builder.Property(a => a.CreatedAt)
                .IsRequired();

            builder.HasOne(a => a.User)
                .WithMany(u => u.ActivityLogs)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}