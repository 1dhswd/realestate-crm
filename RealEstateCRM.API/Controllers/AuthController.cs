using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateCRM.Application.DTOs.Auth;
using RealEstateCRM.Application.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace RealEstateCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var result = await _authService.RegisterAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"REGISTER ERROR: {ex.Message}");
                
                return BadRequest(new
                {
                    message = ex.Message,
                    innerMessage = ex.InnerException?.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var result = await _authService.LoginAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
        [HttpGet("check-email/{email}")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            var exists = await _authService.UserExistsAsync(email);
            return Ok(new { exists });
        }
    }
}