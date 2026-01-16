using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateCRM.Domain.Entities;

namespace RealEstateCRM.Persistence.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.AppointmentDate)
                .IsRequired();

            builder.HasIndex(a => a.AppointmentDate);

            builder.Property(a => a.Duration)
                .HasDefaultValue(60);

            builder.Property(a => a.Location)
                .HasMaxLength(300);

            builder.Property(a => a.Notes)
                .HasColumnType("nvarchar(max)");

            builder.Property(a => a.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(a => a.CreatedAt)
                .IsRequired();

            builder.HasOne(a => a.Lead)
                .WithMany(l => l.Appointments)
                .HasForeignKey(a => a.LeadId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Property)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Agent)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.AgentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}