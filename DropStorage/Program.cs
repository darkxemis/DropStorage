using DropStorage.WebApi.Services;
using Microsoft.Extensions.Logging.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

IWebHostEnvironment environment = builder.Environment;
IConfiguration configuration = builder.Configuration;

// Services
IServiceCollection services = builder.Services;

services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
    builder =>
    {
        builder.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

ILogger logger = NullLogger.Instance;
services.AddSingleton(typeof(ILogger), logger);

// Business
services.AddBusinessServices(configuration);

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
