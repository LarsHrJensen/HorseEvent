using EventSchedulingContext.Domain.Entities;
using EventSchedulingContext.ReadModels;
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
        public DbSet<Event> Events { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClubReadModel> Clubs { get; set; }

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

            // Event
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Level).HasMaxLength(5);
                entity.Property(e => e.Status).HasConversion<string>();

                entity.Property(e => e.StartDate).HasColumnType("date");
                entity.Property(e => e.EndDate).HasColumnType("date");
                entity.Property(e => e.EntryDeadline).HasColumnType("date");
            });

            // Class
            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired();
                entity.Property(c => c.Level).HasMaxLength(5);
                entity.Property(c => c.Price).HasColumnType("decimal(10,2)");
                entity.Property(c => c.Date).HasColumnType("date");

                entity.HasOne(c => c.Event)
                      .WithMany(e => e.Classes)
                      .HasForeignKey(c => c.EventId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            //club read model
            modelBuilder.Entity<ClubReadModel>(entity =>
            {
                entity.ToTable("club_readmodel", "public");

                entity.HasKey(c => c.ClubId);

                entity.Property(c => c.ClubId)
                      .HasColumnName("club_id");

                entity.Property(c => c.Name)
                      .HasColumnName("name")
                      .HasMaxLength(200)
                      .IsRequired();

                entity.Property(c => c.DistrictId).HasColumnName("district_id");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
