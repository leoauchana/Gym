using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web_Domain.Repository;

namespace Web_Infraestructure.Data;

public static class ServiceExtensions
{
    public static void AddInfraestructureDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRepository, Repository.Repository>();
        services.AddDbContext<GymContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ConnectionSqlServer"));
        });
    }
}
