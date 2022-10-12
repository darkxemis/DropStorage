namespace DropStorage.Core.Cors
{
    public static class CorsExtensionss
    {
        private const string PolicyName = "MyPolicy";

        public static void AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: PolicyName,
                builder =>
                {
                    // Any origin allowed
                    builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
                });
            });
        }

        public static void UseCorsPolicy(this IApplicationBuilder app)
        {
            app.UseCors(PolicyName);
        }
    }
}
