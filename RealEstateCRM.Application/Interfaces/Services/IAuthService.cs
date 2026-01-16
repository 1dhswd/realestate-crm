using RealEstateCRM.Application.DTOs.Auth;
using System.Threading.Tasks;

namespace RealEstateCRM.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<bool> UserExistsAsync(string email);
    }
}