using Microsoft.Extensions.Configuration;

namespace DropStorage.WebApi.Services.Extensions
{
    public static class ConfigurationExtensions
    {
        private const string ConnectionStringName = "EFContext";

        private const string AccessTokenDurationInMinutesName = "AccessTokenDurationInMinutes";

        //Email
        private const string GetEmailFromName = "Email:FromAddress";
        private const string GetPasswordEmailName = "Email:Password";
        private const string GetHostEmailName = "Email:Host";
        private const string GetPortEmailName = "Email:Port";

        private const string GetUrlWebName = "UrlWeb";

        public static string GetConnectionString(this IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(ConnectionStringName);
            return connectionString;
        }

        public static int GetAccessTokenDurationInMinutes(this IConfiguration configuration)
        {
            return int.Parse(configuration[AccessTokenDurationInMinutesName]);
        }

        public static string GetEmailFrom(this IConfiguration configuration)
        {
            return configuration[GetEmailFromName];
        }

        public static string GetPasswordEmail(this IConfiguration configuration)
        {
            return configuration[GetPasswordEmailName];
        }

        public static string GetHostEmail(this IConfiguration configuration)
        {
            return configuration[GetHostEmailName];
        }

        public static int GetPortEmail(this IConfiguration configuration)
        {
            return int.Parse(configuration[GetPortEmailName]);
        }

        public static string GetUrlWeb(this IConfiguration configuration)
        {
            return configuration[GetUrlWebName];
        }
    }
}
