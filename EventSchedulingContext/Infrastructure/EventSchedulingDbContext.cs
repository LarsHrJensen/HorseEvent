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

        public DbSet<ClassLevel> ClassLevels { get; set; }
        public DbSet<Disciplin> Disciplines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ---------------------------
            // Disciplin
            // ---------------------------
            modelBuilder.Entity<Disciplin>(entity =>
            {
                entity.ToTable("discipline", "public");

                entity.HasKey(d => d.Id);

                entity.Property(d => d.Id)
                      .HasColumnName("discipline_id");

                entity.Property(d => d.Name)
                      .HasColumnName("name")
                      .HasMaxLength(100)
                      .IsRequired();
            });

            // ---------------------------
            // ClassLevel
            // ---------------------------
            modelBuilder.Entity<ClassLevel>(entity =>
            {
                entity.ToTable("class_level", "public");

                entity.HasKey(c => c.Id);

                entity.Property(c => c.Id)
                      .HasColumnName("class_level_id");

                entity.Property(c => c.Name)
                      .HasMaxLength(150)
                      .IsRequired()
                      .HasColumnName("name");

                entity.Property(c => c.DisciplineId).HasColumnName("discipline_id");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
