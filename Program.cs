using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// DATABASE CONNECTION

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "DefaultConnection is not configured.");

builder.Services.AddDbContext<StudentDbContext>(options =>
    options.UseNpgsql(connectionString));

// CONTROLLERS

builder.Services.AddControllers();

// CORS CONFIGURATION

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// BUILD APPLICATION

var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();