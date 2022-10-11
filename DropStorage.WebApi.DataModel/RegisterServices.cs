using DropStorage.WebApi.DataModel.Core;
using DropStorage.WebApi.DataModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DropStorage.WebApi.DataModel
{
    public static class RegisterServices
    {
        public static IServiceCollection AddDataModel(this IServiceCollection services, string connectionString)
        {
            // Entity Framework
            services.AddDbContext<DropStorageContext>(options => options.UseSqlServer(connectionString));

            // Unit of Work
            services.AddScoped<EFUnitOfWork<DropStorageContext>>();

            return services;
        }
    }
}
