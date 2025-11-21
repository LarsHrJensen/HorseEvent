using ClubContext.Application.Handlers;
using ClubContext.Application.Interfaces;
using ClubContext.Application.Services;
using ClubContext.ClubInfrastructure.Repositories;
using ClubContext.Infrastructure;
using ClubContext.Infrastructure.Repositories;
using EventSchedulingContext.Application.Interfaces;
using EventSchedulingContext.Application.Services;
using EventSchedulingContext.Infrastructure;
using EventSchedulingContext.Infrastructure.Repositories;
using HorseRider.Application.Handlers;
using HorseRider.Application.Handlers.HorseRider.Application.Handlers;
using HorseRider.Application.Interfaces;
using HorseRiderContext.Application.Services;
using HorseRider.Infrastructure.Repositories;
using HorseRiderContext.Application.Interfaces;
using HorseRiderContext.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Interfaces;
using UserManagementContext.Application.Interfaces;
using UserManagementContext.Application.Services;
using UserManagementContext.Infrastructure;
using UserManagementContext.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// 1. Connection strings
var horseRiderConnectionString = builder.Configuration.GetConnectionString("HorseRidersContext");
var clubConnectionString = builder.Configuration.GetConnectionString("ClubDb");
var userConnectionString = builder.Configuration.GetConnectionString("UserManagementDB");
var eventschedulingConnectionString = builder.Configuration.GetConnectionString("EventSchedulingDB");

// 2. DbContext
builder.Services.AddDbContext<ClubDbContext>(options =>
    options.UseNpgsql(clubConnectionString));
builder.Services.AddDbContext<UserManagementDbContext>(options =>
    options.UseNpgsql(userConnectionString));
builder.Services.AddDbContext<EventSchedulingDbContext>(options =>
    options.UseNpgsql(eventschedulingConnectionString));

// Unit of Work registrering
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// 3. Repositories
builder.Services.AddScoped<IDbConnectionFactory>(sp =>
    new SqlDbConnectionFactory(horseRiderConnectionString));


builder.Services.AddScoped<IHorseRepository, HorseRepository>();
builder.Services.AddScoped<IRiderRepository, RiderRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IPostalCodeRepository, PostalCodeRepository>();
builder.Services.AddScoped<IClubCreatedEventHandler, ClubCreatedEventHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<StartListRepository>();
builder.Services.AddScoped<IClassLevelRepository, ClassLevelRepository>();
builder.Services.AddScoped<IDisciplineRepository, DisciplinRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IHorseBreedRepository, HorseBreedRepository>();
builder.Services.AddScoped<IClubReadRepository, ClubReadRepository>();

// 4. Services
builder.Services.AddScoped<IClubService, ClubService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDisciplineService, DisciplinService >();
builder.Services.AddScoped<IClassLevelService, ClassLevelService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IHorseBreedService, HorseBreedService>();

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
