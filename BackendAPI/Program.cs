using HorseRider.Application.Handlers;
using HorseRider.Application.Handlers.HorseRider.Application.Handlers;
using HorseRider.Application.Interfaces;
using HorseRider.Infrastructure.Repositories;

namespace BackendAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Hent connection string fra appsettings.json
            var horseRiderConnectionString = builder.Configuration.GetConnectionString("HorseRidersContext");

            // Registrer repository med vanilla SQL
            builder.Services.AddScoped<IRiderRepository>(provider =>
                new RiderRepository(horseRiderConnectionString));

            builder.Services.AddScoped<IHorseRepository>(provider =>
                new HorseRepository(horseRiderConnectionString));

            // Registrer command handlers
            builder.Services.AddScoped<CreateRiderHandler>();
            builder.Services.AddScoped<CreateHorseHandler>();


            // Tilføj MediatR (scanner hele Application-laget for handlers)
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

