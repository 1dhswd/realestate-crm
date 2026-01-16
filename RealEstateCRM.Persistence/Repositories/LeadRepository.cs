using Microsoft.EntityFrameworkCore;
using RealEstateCRM.Application.Interfaces.Repositories;
using RealEstateCRM.Domain.Entities;
using RealEstateCRM.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCRM.Persistence.Repositories
{
    public class LeadRepository : GenericRepository<Lead>, ILeadRepository
    {
        public LeadRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Lead>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                .Where(x => !x.IsDeleted)
                .Include(l => l.Customer)
                .Include(l => l.Property)
                .Include(l => l.Status)
                .Include(l => l.CreatedByUser)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }


        public async Task<IEnumerable<Lead>> GetLeadsByStatusAsync(int statusId)
        {
            return await _dbSet
                .Where(l => l.StatusId == statusId && !l.IsDeleted)
                .Include(l => l.Customer)
                .Include(l => l.Property)
                .Include(l => l.Status)
                .Include(l => l.CreatedByUser)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lead>> GetLeadsByUserAsync(int userId)
        {
            return await _dbSet
                .Where(l => l.CreatedByUserId == userId && !l.IsDeleted)
                .Include(l => l.Customer)
                .Include(l => l.Property)
                .Include(l => l.Status)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }

        public async Task<Lead> GetLeadWithDetailsAsync(int id)
        {
            return await _dbSet
                .Where(l => l.Id == id && !l.IsDeleted)
                .Include(l => l.Customer)
                .Include(l => l.Property)
                .Include(l => l.Status)
                .Include(l => l.CreatedByUser)
                .Include(l => l.Appointments)
                .Include(l => l.Offers)
                .FirstOrDefaultAsync();
        }
    }
}