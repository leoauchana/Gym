using Microsoft.Extensions.DependencyInjection;
using Web_Domain.Repository;
using Web_Infraestructure.Data.Repository;

namespace Web_Application;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository, Repository>();
    }
}
