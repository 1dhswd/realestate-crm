using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateCRM.Domain.Entities;

namespace RealEstateCRM.Persistence.Configurations
{
    public class PropertyPropertyFeatureConfiguration : IEntityTypeConfiguration<PropertyPropertyFeature>
    {
        public void Configure(EntityTypeBuilder<PropertyPropertyFeature> builder)
        {
            builder.ToTable("PropertyPropertyFeatures");

            builder.HasKey(ppf => new { ppf.PropertyId, ppf.FeatureId });

            builder.HasOne(ppf => ppf.Property)
                .WithMany(p => p.PropertyFeatures)
                .HasForeignKey(ppf => ppf.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ppf => ppf.Feature)
                .WithMany(f => f.PropertyPropertyFeatures)
                .HasForeignKey(ppf => ppf.FeatureId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}