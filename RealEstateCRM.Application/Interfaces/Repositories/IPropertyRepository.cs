using RealEstateCRM.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Interfaces.Repositories
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {
        Task<IEnumerable<Property>> GetActivePropertiesAsync();
        Task<IEnumerable<Property>> GetPropertiesByCategoryAsync(int categoryId);
        Task<IEnumerable<Property>> GetPropertiesByCityAsync(string city);

        Task<IEnumerable<Property>> GetAllWithDetailsAsync();
        Task<Property> GetPropertyWithDetailsAsync(int id);
        Task IncrementViewCountAsync(int id);
    }
}