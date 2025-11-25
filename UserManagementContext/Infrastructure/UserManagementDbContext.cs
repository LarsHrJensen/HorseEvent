using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagementContext.Domain.Entities;

namespace UserManagementContext.Infrastructure;

public class UserManagementDbContext : IdentityDbContext<UserEntity>
{
    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
}