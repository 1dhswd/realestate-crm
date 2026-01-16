using RealEstateCRM.Application.DTOs.Auth;
using RealEstateCRM.Application.Interfaces.Repositories;
using RealEstateCRM.Application.Interfaces.Services;
using RealEstateCRM.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCRM.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        public AuthService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            if (await _userRepository.EmailExistsAsync(dto.Email))
            {
                throw new Exception("Bu email adresi zaten kullanılıyor");
            }

            var user = new User
            {
                Username = dto.Username,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = PasswordHasher.HashPassword(dto.Password),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            user.UserRoles = new[]
            {
                new UserRole
                {
                    UserId = user.Id,
                    RoleId = 2, 
                    AssignedAt = DateTime.UtcNow
                }
            };

            await _userRepository.UpdateAsync(user);

            var userWithRoles = await _userRepository.GetUserWithRolesAsync(user.Id);
            var token = _jwtService.GenerateToken(userWithRoles);

            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                Token = token,
                Roles = userWithRoles.UserRoles.Select(ur => ur.Role.Name).ToArray()
            };
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByUsernameAsync(dto.Username);

            if (user == null)
                throw new Exception("Username veya şifre hatalı");

            if (!PasswordHasher.VerifyPassword(user.PasswordHash, dto.Password))
                throw new Exception("Username veya şifre hatalı");

            if (!user.IsActive)
                throw new Exception("Hesabınız pasif durumda");

            user.LastLoginAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            var userWithRoles = await _userRepository.GetUserWithRolesAsync(user.Id);
            var token = _jwtService.GenerateToken(userWithRoles);

            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                Token = token,
                Roles = userWithRoles.UserRoles.Select(x => x.Role.Name).ToArray()
            };
        }
        public async Task<bool> UserExistsAsync(string email)
        {
            return await _userRepository.EmailExistsAsync(email);
        }
    }
}