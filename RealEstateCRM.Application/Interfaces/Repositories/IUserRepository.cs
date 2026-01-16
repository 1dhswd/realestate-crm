using RealEstateCRM.Domain.Entities;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetUserWithRolesAsync(int id);
        Task<bool> EmailExistsAsync(string email);
        Task<User> GetByUsernameAsync(string username);

    }
}