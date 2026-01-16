using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateCRM.Domain.Entities;

namespace RealEstateCRM.Persistence.Configurations
{
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.ToTable("Offers");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.OfferedPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Message)
                .HasColumnType("nvarchar(max)");

            builder.Property(o => o.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(o => o.ValidUntil)
                .IsRequired();

            builder.Property(o => o.CreatedAt)
                .IsRequired();

            builder.HasOne(o => o.Lead)
                .WithMany(l => l.Offers)
                .HasForeignKey(o => o.LeadId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Property)
                .WithMany(p => p.Offers)
                .HasForeignKey(o => o.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.CreatedByUser)
                .WithMany(u => u.CreatedOffers)
                .HasForeignKey(o => o.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}