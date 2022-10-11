using Microsoft.Extensions.Configuration;

namespace DropStorage.WebApi.Services.Extensions
{
    public static class ConfigurationExtensions
    {
        private const string ConnectionStringName = "EFContext";

        public static string GetConnectionString(this IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(ConnectionStringName);
            return connectionString;
        }
    }
}
