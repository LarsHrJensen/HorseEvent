using ClubContext.Application.Handlers;
using ClubContext.Application.Interfaces;
using ClubContext.Application.Services;
using ClubContext.ClubInfrastructure.Repositories;
using ClubContext.Infrastructure.Repositories;
using ClubContext.Infrastructure;
using HorseRider.Application.Handlers.HorseRider.Application.Handlers;
using HorseRider.Application.Handlers;
using HorseRider.Application.Interfaces;
using HorseRiderContext.Infrastructure.Repositories;
using HorseRider.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Interfaces;
using UserManagementContext.Infrastructure.Repositories;
using UserManagementContext.Application.Interfaces;
using UserManagementContext.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Connection strings
var horseRiderConnectionString = builder.Configuration.GetConnectionString("HorseRidersContext");
var clubConnectionString = builder.Configuration.GetConnectionString("ClubDb");

// 2. DbContext
builder.Services.AddDbContext<ClubDbContext>(options =>
    options.UseNpgsql(clubConnectionString));

// 3. Repositories
builder.Services.AddScoped<IDbConnectionFactory>(sp =>
    new SqlDbConnectionFactory(horseRiderConnectionString));

builder.Services.AddScoped<IHorseRepository, HorseRepository>();
builder.Services.AddScoped<IRiderRepository, RiderRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IPostalCodeRepository, PostalCodeRepository>();
builder.Services.AddScoped<IClubRepository, ClubRepository>(); 
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<StartListRepository>();
builder.Services.AddScoped<RiderPerformanceRepository>();

// 4. Services
builder.Services.AddScoped<IClubService, ClubService>();
builder.Services.AddScoped<IUserService, UserService>();

// 5. Command / Query Handlers (hvis du bruger MediatR)
builder.Services.AddScoped<CreateRiderHandler>();
builder.Services.AddScoped<CreateHorseHandler>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateRiderHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetCountriesHandler).Assembly);
});

// 6. Controllers
builder.Services.AddControllers();

// 7. OpenAPI / Swagger
builder.Services.AddOpenApi();

// 8. CORS
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

// Middleware
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
