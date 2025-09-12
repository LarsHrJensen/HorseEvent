using Microsoft.AspNetCore.Builder;

namespace HorseEvent
{
    public class Program
    {
           public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ////// Tilføj DbContext (PostgreSQL)
            //builder.Services.AddDbContext<RiderDbContext>(options =>
            //    options.UseNpgsql(builder.Configuration.GetConnectionString("HorseRiderDB")));

            //// Registrer repository
            //builder.Services.AddScoped<IRiderRepository, RiderRepository>();

            //// Registrer command handlers
            //builder.Services.AddScoped<CreateRiderCommandHandler>();

            ////Tilføj MediatR(scanner hele Application-laget for handlers)
            //builder.Services.AddMediatR(cfg =>
            //{
            //    cfg.RegisterServicesFromAssembly(typeof(CreateRiderCommandHandler).Assembly);
            //});

            builder.Services.AddControllers();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:3002", "http://localhost:3003") // Next.js frontend
                           .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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

