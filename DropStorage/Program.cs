using DropStorage.Core.Cors;
using DropStorage.Core.Exceptions;
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

services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy",
    builder =>
    {
            // Any origin allowed
            builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
    });
});

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
if (environment.IsDevelopment())
{
    app.UseErrorControllerAsExceptionHandler();
    // app.UseDeveloperExceptionPage(); // descomentar para que devuelva la traza del error
}
else
{
    app.UseErrorControllerAsExceptionHandler();
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
