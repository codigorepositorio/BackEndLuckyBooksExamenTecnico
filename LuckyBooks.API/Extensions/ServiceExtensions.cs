using Bussines;
using Data;
using LoggerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LuckyBooks.API.Extensions
{
    public static class ServiceExtensions
    {   
        public static IApplicationBuilder ConfigureCors(this IApplicationBuilder app)
        {
            app.UseCors(builder =>
             builder.WithOrigins("http://localhost:4200", "http://localhost:4201")
             .AllowAnyHeader()
              .AllowAnyMethod());
            return app;
        }
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager,LoggerManager>();
        }
        public static void ConfigureServices(this IServiceCollection services)
        {
            //LibroRepository Service
            services.AddScoped<ILibroRepository, LibroRepository>();
            services.AddScoped<LibroService>();


            //Asignatura service
            services.AddScoped<IAsignaturaRepository, AsignaturaRepository>();
            services.AddScoped<AsignaturaService>();
        }
    }
}

