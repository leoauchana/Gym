using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Web_Application;
using Web_Infraestructure.Data;
using Web_Presentation.Middleware;
namespace Web_Presentation;

public class Program
{
    /*
     * Correcciones a hacer
     */
    //TODO: Terminar de hacer los logs y verificar los metodos de los controladores de dashboard
    //TODO: Terminar de adaptar el middeware a los controladores y también verificar si las excepciones establecidas están bien, es decir si es una buena practica las excepciones
    //TODO: Verificar si en los casos que estableci es necesario lanzar excepciones o no
    

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
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddInfraestructureDataServices(builder.Configuration);


        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseMiddleware<ExceptionMiddlware>();

        app.MapControllers();

        app.Run();
    }
}