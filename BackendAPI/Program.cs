using HorseRider.Application.Handlers;
using HorseRider.Application.Handlers.HorseRider.Application.Handlers;
using HorseRider.Application.Interfaces;
using HorseRider.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BackendAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Hent connection string fra appsettings.json
            var horseRiderConnectionString = builder.Configuration.GetConnectionString("HorseRidersContext");

            // Registrer DbConnectionFactory (kan bruges af alle repositories)
            builder.Services.AddScoped<IDbConnectionFactory>(sp =>
                new SqlDbConnectionFactory(horseRiderConnectionString));

            // Registrer repositories
            builder.Services.AddScoped<IHorseRepository, HorseRepository>();
            builder.Services.AddScoped<IRiderRepository, RiderRepository>();

            // Registrer command handlers
            builder.Services.AddScoped<CreateRiderHandler>();
            builder.Services.AddScoped<CreateHorseHandler>();

            // Tilføj MediatR (scanner Application-laget for handlers)
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateRiderHandler).Assembly);
            });

            builder.Services.AddControllers();

            // OpenAPI / Swagger
            builder.Services.AddOpenApi();

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:3000", "http://localhost:3001",
                                       "http://localhost:3002", "http://localhost:3003")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseCors();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}

