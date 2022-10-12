using DropStorage.Core.Cors;
using DropStorage.Core.Jwt;
using DropStorage.Core.Swagger;
using DropStorage.WebApi.Services;
using Microsoft.Extensions.Logging.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

IWebHostEnvironment environment = builder.Environment;
IConfiguration configuration = builder.Configuration;

// Services
IServiceCollection services = builder.Services;

ILogger logger = NullLogger.Instance;
services.AddSingleton(typeof(ILogger), logger);

// Business
services.AddBusinessServices(configuration);

// Cors
services.AddCorsPolicy();

// Swagger
services.AddSwagger();

//JWT
services.AddJwtAuthentication(configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error-local-development"); // Add this
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
