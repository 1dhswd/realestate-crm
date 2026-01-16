using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateCRM.Domain.Entities;

namespace RealEstateCRM.Persistence.Configurations
{
    public class LeadConfiguration : IEntityTypeConfiguration<Lead>
    {
        public void Configure(EntityTypeBuilder<Lead> builder)
        {
            builder.ToTable("Leads");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Budget)
                .HasColumnType("decimal(18,2)");

            builder.Property(l => l.Notes)
                .HasColumnType("nvarchar(max)");

            builder.Property(l => l.CreatedAt)
                .IsRequired();

            builder.HasIndex(l => l.StatusId);

            builder.HasOne(l => l.Customer)
                .WithMany(c => c.Leads)
                .HasForeignKey(l => l.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.Property)
                .WithMany(p => p.Leads)
                .HasForeignKey(l => l.PropertyId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(l => l.Customer)
                .WithMany(c => c.Leads) 
                .HasForeignKey(l => l.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);



            builder.HasOne(l => l.CreatedByUser)
                .WithMany(u => u.CreatedLeads)
                .HasForeignKey(l => l.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(l => l.Appointments)
                .WithOne(a => a.Lead)
                .HasForeignKey(a => a.LeadId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(l => l.Offers)
                .WithOne(o => o.Lead)
                .HasForeignKey(o => o.LeadId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}