using ClubContext.Domain.Entities;
using ClubContext.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace ClubContext.Infrastructure
{
    public class ClubDbContext : DbContext
    {
        public ClubDbContext(DbContextOptions<ClubDbContext> options)
            : base(options)
        {
        }

        // Reference-tabeller
        public DbSet<Country> Countries { get; set; }
        public DbSet<PostalCodeCity> PostalCodeCities { get; set; } // kun DK-postnumre
        public DbSet<District> Districts { get; set; }

        public DbSet<OutboxEvent> Outbox { get; set; }

        // Hovedtabel
        public DbSet<Club> Clubs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // ---------------------------
            // Outbox-konfiguration
            // ---------------------------
            modelBuilder.Entity<OutboxEvent>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.EventType).IsRequired();
                entity.Property(e => e.Payload).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.Processed).IsRequired();
            });

            // ---------------------------
            // Country-konfiguration
            // ---------------------------
            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("countries");
                entity.HasKey(c => c.Code);
                entity.Property(c => c.Code)
                      .HasColumnType("char(2)")
                      .IsRequired()
                      .HasColumnName("country_code");
                entity.Property(c => c.Name)
                      .HasMaxLength(100)
                      .HasColumnName("country_name")
                      .IsRequired();
            });

            // ---------------------------
            // PostalCodeCity-konfiguration
            // ---------------------------
            modelBuilder.Entity<PostalCodeCity>(entity =>
            {
                entity.ToTable("postal_codes");
                entity.HasKey(p => p.PostalCode);

                entity.Property(p => p.PostalCode)
                      .HasMaxLength(4)
                      .IsRequired();

                entity.Property(p => p.City)
                      .HasMaxLength(100)
                      .IsRequired();
            });

            // ---------------------------
            // District-konfiguration
            // ---------------------------
            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("district");
                entity.HasKey(d => d.DistrictId);

                entity.Property(d => d.Name)
                      .HasMaxLength(100)
                      .IsRequired();
            });

            // ---------------------------
            // Club-konfiguration med denormaliseret adresse
            // ---------------------------
            modelBuilder.Entity<Club>(entity =>
            {
                entity.ToTable("clubs");
                entity.HasKey(c => c.ClubId); // Kun definerer nøglen

                entity.Property(c => c.ClubId)   // Her mapper vi selve kolonnen
                      .HasColumnName("id");


                entity.Property(c => c.Name)
                 .HasColumnName("name")
                      .HasMaxLength(255)
                      .IsRequired();

                // --- FK til District (nullable) ---
                entity.Property<int?>("DistrictId")
                      .HasColumnName("district_id");

                entity.HasOne<District>()
                      .WithMany()
                      .HasForeignKey("DistrictId")
                      .IsRequired(false)        // <-- optional relation
                      .OnDelete(DeleteBehavior.SetNull);

                // Adresse som value object, gemt som kolonner
                entity.OwnsOne(c => c.Adress, address =>
                {
                    address.Property(a => a.StreetName)
                           .HasMaxLength(255)
                           .IsRequired()
                           .HasColumnName("street_name");

                    address.Property(a => a.HouseNumber)
                           .HasMaxLength(50)
                           .IsRequired()
                           .HasColumnName("street_number");

                    address.Property(a => a.Apartment)
                           .HasMaxLength(50)
                           .HasColumnName("apartment");

                    address.Property(a => a.PostalCode)
                           .HasMaxLength(20)
                           .IsRequired()
                           .HasColumnName("postal_code");

                    address.Property(a => a.City)
                           .HasMaxLength(100)
                           .IsRequired()
                           .HasColumnName("city");

                    address.Property(a => a.CountryCode)
                           .HasColumnType("char(2)")
                           .IsRequired()
                           .HasColumnName("country_code");

                    address.Property(a => a.CountryName)
                           .HasMaxLength(100)
                           .IsRequired()
                           .HasColumnName("country_name");
                });
            });
        }
    }
}
