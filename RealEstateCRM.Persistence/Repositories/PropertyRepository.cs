using Microsoft.EntityFrameworkCore;
using RealEstateCRM.Application.Interfaces.Repositories;
using RealEstateCRM.Domain.Entities;
using RealEstateCRM.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCRM.Persistence.Repositories
{
    public class PropertyRepository : GenericRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Property>> GetActivePropertiesAsync()
        {
            return await _dbSet
                .Where(p => p.IsActive)
                .Include(p => p.Category)
                .Include(p => p.Type)
                .Include(p => p.Owner)
                .Include(p => p.Images)
                .Include(p => p.PropertyFeatures)
                    .ThenInclude(pf => pf.Feature)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                .Include(p => p.Category)
                .Include(p => p.Type)
                .Include(p => p.Owner)
                .Include(p => p.PropertyFeatures)
                    .ThenInclude(pf => pf.Feature)
                .Include(p => p.Images)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }


        public async Task<IEnumerable<Property>> GetPropertiesByCategoryAsync(int categoryId)
        {
            return await _dbSet
                .Where(p => p.CategoryId == categoryId && p.IsActive)
                .Include(p => p.Category)
                .Include(p => p.Type)
                .Include(p => p.Owner)
                .Include(p => p.Images)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetPropertiesByCityAsync(string city)
        {
            return await _dbSet
                .Where(p => p.City.ToLower() == city.ToLower() && p.IsActive)
                .Include(p => p.Category)
                .Include(p => p.Type)
                .Include(p => p.Owner)
                .Include(p => p.Images)
                .ToListAsync();
        }

        public async Task<Property> GetPropertyWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Include(p => p.Type)
                .Include(p => p.Owner)
                .Include(p => p.Images)
                .Include(p => p.PropertyFeatures)
                    .ThenInclude(pf => pf.Feature)
                .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task IncrementViewCountAsync(int id)
        {
            var property = await _dbSet.FindAsync(id);
            if (property != null)
            {
                property.ViewCount++;
                await _context.SaveChangesAsync();
            }
        }
    }
}