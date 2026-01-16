using RealEstateCRM.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Interfaces.Repositories
{
    public interface ILeadRepository : IGenericRepository<Lead>
    {
        Task<IEnumerable<Lead>> GetLeadsByStatusAsync(int statusId);
        Task<IEnumerable<Lead>> GetLeadsByUserAsync(int userId);
        Task<Lead> GetLeadWithDetailsAsync(int id);
        Task<IEnumerable<Lead>> GetAllWithDetailsAsync();
    }
}