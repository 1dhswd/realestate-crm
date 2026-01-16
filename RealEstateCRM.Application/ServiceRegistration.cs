using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RealEstateCRM.Application.Behaviors;
using System.Reflection;

namespace RealEstateCRM.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assembly);

            services.AddMediatR(assembly);

            services.AddValidatorsFromAssembly(assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}