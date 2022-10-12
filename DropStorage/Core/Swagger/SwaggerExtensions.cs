using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace DropStorage.Core.Swagger
{
    public static class SwaggerExtensions
    {
        private const int Version = 1;

        private const string Name = "Eurofred IoT API";

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v{Version}", new OpenApiInfo { Title = Name, Version = $"v{Version}" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });

                c.OperationFilter<AddAuthorizationHeaderOperationHeader>();

                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            });
        }

        public static void UseCustomSwagger(this IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            //Habilitar swagger
            app.UseSwagger();

            //indica la ruta para generar la configuración de swagger
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = $"{Name} {hostingEnvironment.EnvironmentName}";
                options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Name} v{Version}");
                options.DefaultModelsExpandDepth(-1);
            });
        }
    }
}
