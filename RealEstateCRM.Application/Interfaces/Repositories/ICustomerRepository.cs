using RealEstateCRM.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Interfaces.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetCustomersByAgentAsync(int agentId);
        Task<Customer> GetCustomerWithLeadsAsync(int id);

        Task<IEnumerable<Customer>> GetAllWithStatusAsync();

    }
}