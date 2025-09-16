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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("countries");
                entity.HasKey(c => c.Code);
                entity.Property(c => c.Code).HasColumnType("char(2)").IsRequired();
                entity.Property(c => c.Name).HasMaxLength(100).IsRequired();
            });
        }
    }
}
    
