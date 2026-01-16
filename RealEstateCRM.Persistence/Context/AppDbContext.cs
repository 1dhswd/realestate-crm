using Microsoft.EntityFrameworkCore;
using RealEstateCRM.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateCRM.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<PropertyCategory> PropertyCategories { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyFeature> PropertyFeatures { get; set; }
        public DbSet<PropertyPropertyFeature> PropertyPropertyFeatures { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerStatus> CustomerStatuses { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<LeadStatus> LeadStatuses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Domain.Common.BaseEntity &&
                       (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (Domain.Common.BaseEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}