using ClubContext.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace ClubContext.Infrastructure
{
    public class ClubDbContext : DbContext
    {
        public ClubDbContext(DbContextOptions<ClubDbContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<PostalCodeCity> PostalCodeCities { get; set; } // kun DK-postnumre

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Country-konfiguration
            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("countries");
                entity.HasKey(c => c.Code);
                entity.Property(c => c.Code)
                      .HasColumnType("char(2)")
                      .IsRequired();
                entity.Property(c => c.Name)
                      .HasMaxLength(100)
                      .IsRequired();
            });

            // PostalCodeCity-konfiguration
            modelBuilder.Entity<PostalCodeCity>(entity =>
            {
                entity.ToTable("postal_codes");

                // Brug Code som primærnøgle (value object identificeres af koden)
                entity.HasKey(p => p.PostalCode);

                entity.Property(p => p.PostalCode)
                      .HasMaxLength(4)
                      .IsRequired();

                entity.Property(p => p.City)
                      .HasMaxLength(100)
                      .IsRequired();
            });
        }
    }
}
    
