using Microsoft.Extensions.DependencyInjection;
using DropStorage.WebApi.DataModel;
using DropStorage.WebApi.ServicesDataAccess.DataAccess;
using AutoMapper;
using DropStorage.WebApi.ServicesDataAccess.Mappers;

namespace DropStorage.WebApi.ServicesDataAccess
{
    public static class RegisterServices
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
        {
            // Data Model
            services.AddDataModel(connectionString);

            // Data Access
            services = RegisterDataAccess(services);

            return services;
        }

        public static IServiceCollection RegisterDataAccess(this IServiceCollection services)
        {
            // Auto Mapper
            MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new RolMapper());
                mc.AddProfile(new UserMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Data Access
            services.AddTransient<UserDataAccess>();
            services.AddTransient<LogStatusDataAccess>();
            
            return services;
        }
    }
}
