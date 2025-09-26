using Microsoft.AspNetCore.Mvc;

namespace MinimalAlumnsManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddTransient<Connection>();
            builder.Services.AddTransient<Consulta>();
            var app = builder.Build();

            app.MapGet("/", async (Connection connection) =>
            {
                return ("hola");

            });

            app.MapPost("/teacher", async ([FromBody]ModelDto model, Consulta consulta) =>
            {
                var resultado = await consulta.InsertTeacher(model);
                if (resultado) return ("Registrado correctamente");
                return ("Error al registrar");
            });

            app.MapGet("/getAll", async (Consulta consulta) =>
            {
                return await consulta.GetAll();
            });

            app.Run();
        }
    }
}
