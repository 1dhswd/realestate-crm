using Microsoft.EntityFrameworkCore;
using RealEstateCRM.Application.Interfaces.Repositories;
using RealEstateCRM.Domain.Entities;
using RealEstateCRM.Persistence.Context;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCRM.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User> GetUserWithRolesAsync(int id)
        {
            return await _dbSet
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Username == username);
        }


        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbSet
                .AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }
    }
}