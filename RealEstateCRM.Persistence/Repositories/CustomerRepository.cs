using Microsoft.EntityFrameworkCore;
using RealEstateCRM.Application.Interfaces.Repositories;
using RealEstateCRM.Domain.Entities;
using RealEstateCRM.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCRM.Persistence.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Customer>> GetCustomersByAgentAsync(int agentId)
        {
            return await _dbSet
                .Where(c => c.AssignedAgentId == agentId)
                .Include(c => c.AssignedAgent)
                .Include(c => c.Status)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllWithStatusAsync()
        {
            return await _dbSet
                .Include(c => c.AssignedAgent)
                .Include(c => c.Status)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }



        public async Task<Customer> GetCustomerWithLeadsAsync(int id)
        {
            return await _dbSet
                .Include(c => c.AssignedAgent)
                .Include(c => c.Leads)

                    .ThenInclude(l => l.Status)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}