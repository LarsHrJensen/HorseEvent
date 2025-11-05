using EventSchedulingContext.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventSchedulingContext.Infrastructure
{
    public class EventSchedulingDbContext : DbContext
    {
        public EventSchedulingDbContext(DbContextOptions<EventSchedulingDbContext> options)
            : base(options)
        {
        }

        public DbSet<ClassCategory> classCategories { get; set; }
        public DbSet<Disciplin> disciplins { get; set; }
    }
}
