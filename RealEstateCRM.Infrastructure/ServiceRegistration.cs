using Microsoft.Extensions.DependencyInjection;
using RealEstateCRM.Application.Interfaces.Services;
using RealEstateCRM.Infrastructure.Services;

namespace RealEstateCRM.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}