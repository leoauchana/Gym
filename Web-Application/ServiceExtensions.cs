using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Web_Application.Interfaces;
using Web_Application.Services;
using Web_Application.Validators;
using Web_Domain.Repository;
using Web_Infraestructure.Data.Repository;

namespace Web_Application;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRepository, Repository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IPayService, PayService>();
        services.AddScoped<IDashboarService, DashboardService>();
        services.AddValidatorsFromAssemblyContaining<UserDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<EmployeeDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<ClientDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<DashboardDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<PayDtoValidator>();
        services.AddAuthentication(config =>
        {
            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(config =>
        {
            config.RequireHttpsMetadata = false;
            config.SaveToken = true;
            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = false,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]!))
            };
        });

    }
}
