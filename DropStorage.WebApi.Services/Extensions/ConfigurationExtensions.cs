using Microsoft.Extensions.Configuration;

namespace DropStorage.WebApi.Services.Extensions
{
    public static class ConfigurationExtensions
    {
        private const string ConnectionStringName = "EFContext";

        private const string AccessTokenDurationInMinutesName = "AccessTokenDurationInMinutes";

        public static string GetConnectionString(this IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(ConnectionStringName);
            return connectionString;
        }

        public static int GetAccessTokenDurationInMinutes(this IConfiguration configuration)
        {
            return int.Parse(configuration[AccessTokenDurationInMinutesName]);
        }
    }
}
