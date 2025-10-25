using Microsoft.EntityFrameworkCore;
using UserManagementContext.Domain.Entities;

namespace UserManagementContext.Infrastructure
{
    public class UserManagementDbContext : DbContext
    {
        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguration af UserEntity
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("users", "public"); // Navngivning af tabel i PostgreSQL

                entity.HasKey(e => e.Id); // Primærnøgle

                entity.Property(e => e.Id)
                      .HasColumnName("id")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Username)
                      .HasColumnName("username")
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Email)
                      .HasColumnName("email")
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(e => e.PasswordHash)
                      .HasColumnName("password_hash")
                      .IsRequired()
                      .HasMaxLength(512);

                entity.Property(e => e.Salt)
                      .HasColumnName("salt")
                      .IsRequired()
                      .HasMaxLength(128);

                entity.Property(e => e.MemberId)
                      .HasColumnName("member_id")
                      .IsRequired();
            });
        }
    }
}
