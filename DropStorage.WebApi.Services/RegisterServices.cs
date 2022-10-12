using DropStorage.WebApi.Services.Extensions;
using DropStorage.WebApi.Services.Services;
using DropStorage.WebApi.Services.Services.AuthServices;
using DropStorage.WebApi.ServicesDataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DropStorage.WebApi.Services
{
    public static class RegisterServices
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Data Access
            services.AddDataAccess(configuration.GetConnectionString());

            // Business
            services = RegisterBusinessServices(services);

            return services;
        }

        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
        {
            // Auth
            services.AddTransient<JwtTokenService>();

            // Business
            services.AddTransient<UserService>();

            return services;
        }
    }
}
