using RealEstateCRM.Domain.Entities;

namespace RealEstateCRM.Application.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}