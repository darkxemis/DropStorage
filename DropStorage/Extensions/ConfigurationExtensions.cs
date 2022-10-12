namespace DropStorage.Extensions
{
    public static class ConfigurationExtensions
    {
        private const string JwtKey = "Jwt:Key";

        public static string GetJwtKey(this IConfiguration configuration)
        {
            return configuration[JwtKey];
        }
    }
}
