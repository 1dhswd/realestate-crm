using RealEstateCRM.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCRM.Persistence.Context
{
    public static class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roles = new[]
                {
                    new Role { Name = "Admin", Description = "Sistem yöneticisi - Tüm yetkiler", CreatedAt = DateTime.UtcNow },
                    new Role { Name = "Agent", Description = "Gayrimenkul danışmanı", CreatedAt = DateTime.UtcNow },
                    new Role { Name = "Manager", Description = "Bölge müdürü", CreatedAt = DateTime.UtcNow }
                };
                context.Roles.AddRange(roles);
                await context.SaveChangesAsync();
            }

            if (!context.PropertyCategories.Any())
            {
                var categories = new[]
                {
                    new PropertyCategory { Name = "Daire", Description = "Apartman daireleri", IsActive = true, CreatedAt = DateTime.UtcNow },
                    new PropertyCategory { Name = "Villa", Description = "Müstakil villalar", IsActive = true, CreatedAt = DateTime.UtcNow },
                    new PropertyCategory { Name = "Arsa", Description = "İmar ve arazi", IsActive = true, CreatedAt = DateTime.UtcNow },
                    new PropertyCategory { Name = "İşyeri", Description = "Dükkan, ofis, vs.", IsActive = true, CreatedAt = DateTime.UtcNow }
                };
                context.PropertyCategories.AddRange(categories);
                await context.SaveChangesAsync();
            }

            if (!context.PropertyTypes.Any())
            {
                var types = new[]
                {
                    new PropertyType { Name = "Satılık", IsActive = true, CreatedAt = DateTime.UtcNow },
                    new PropertyType { Name = "Kiralık", IsActive = true, CreatedAt = DateTime.UtcNow }
                };
                context.PropertyTypes.AddRange(types);
                await context.SaveChangesAsync();
            }

            if (!context.PropertyFeatures.Any())
            {
                var features = new[]
                {
                    new PropertyFeature { Name = "Asansör", Icon = "elevator", IsActive = true, CreatedAt = DateTime.UtcNow },
                    new PropertyFeature { Name = "Otopark", Icon = "local_parking", IsActive = true, CreatedAt = DateTime.UtcNow },
                    new PropertyFeature { Name = "Güvenlik", Icon = "security", IsActive = true, CreatedAt = DateTime.UtcNow },
                    new PropertyFeature { Name = "Havuz", Icon = "pool", IsActive = true, CreatedAt = DateTime.UtcNow },
                    new PropertyFeature { Name = "Spor Salonu", Icon = "fitness_center", IsActive = true, CreatedAt = DateTime.UtcNow },
                    new PropertyFeature { Name = "Ebeveyn Banyosu", Icon = "bathroom", IsActive = true, CreatedAt = DateTime.UtcNow },
                    new PropertyFeature { Name = "Balkon", Icon = "balcony", IsActive = true, CreatedAt = DateTime.UtcNow },
                    new PropertyFeature { Name = "Klima", Icon = "ac_unit", IsActive = true, CreatedAt = DateTime.UtcNow }
                };
                context.PropertyFeatures.AddRange(features);
                await context.SaveChangesAsync();
            }

            if (!context.CustomerStatuses.Any())
            {
                var statuses = new[]
                {
                    new CustomerStatus { Name = "Yeni Lead", ColorCode = "#3498db", DisplayOrder = 1, CreatedAt = DateTime.UtcNow },
                    new CustomerStatus { Name = "İletişimde", ColorCode = "#f39c12", DisplayOrder = 2, CreatedAt = DateTime.UtcNow },
                    new CustomerStatus { Name = "Görüşme Yapıldı", ColorCode = "#9b59b6", DisplayOrder = 3, CreatedAt = DateTime.UtcNow },
                    new CustomerStatus { Name = "Teklif Verildi", ColorCode = "#e67e22", DisplayOrder = 4, CreatedAt = DateTime.UtcNow },
                    new CustomerStatus { Name = "Kazanıldı", ColorCode = "#27ae60", DisplayOrder = 5, CreatedAt = DateTime.UtcNow },
                    new CustomerStatus { Name = "Kaybedildi", ColorCode = "#e74c3c", DisplayOrder = 6, CreatedAt = DateTime.UtcNow }
                };
                context.CustomerStatuses.AddRange(statuses);
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                var adminUser = new User
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@realestate.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEJ8VKqvxQz5v9nKd8xjdGLj3Qv8CnKP6F4Y7mW2Ln9Rh4Xp5Zt1Ks3Vw8Yx2Qa7Zb4=", 
                    PhoneNumber = "+90 555 000 0001",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };
                context.Users.Add(adminUser);
                await context.SaveChangesAsync();

                var adminRole = context.Roles.First(r => r.Name == "Admin");
                context.UserRoles.Add(new UserRole
                {
                    UserId = adminUser.Id,
                    RoleId = adminRole.Id,
                    AssignedAt = DateTime.UtcNow
                });
                await context.SaveChangesAsync();
            }
        }
    }
}