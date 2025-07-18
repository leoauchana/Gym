using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Web_Application;
using Web_Infraestructure.Data;
namespace Web_Presentation;

public class Program
{
    /*
     * Correcciones a hacer
     */
    //TODO: Revisar las relaciones de las entidades, para poder implementar un log de registro de clientes
    //TODO: Terminar de hacer los logs y verificar los metodos de los controladores de dashboard
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        });
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Administrator", policy =>
            {
                policy.RequireRole("Admin");
            });
            options.AddPolicy("Coach", policy =>
            {
                policy.RequireRole("Coach");
            });
            options.AddPolicy("AdminAndReceptionist", policy =>
            {
                policy.RequireRole("Admin", "Receptionist");
            });
        });
        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddInfraestructureDataServices(builder.Configuration);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}