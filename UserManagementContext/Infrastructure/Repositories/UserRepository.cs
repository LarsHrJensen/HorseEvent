using ClubContext.Domain.Entities;
using ClubContext.Infrastructure;
using Microsoft.EntityFrameworkCore;
using UserManagementContext.Application.Interfaces;
using UserManagementContext.Domain.Entities;

namespace UserManagementContext.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagementDbContext _dbContext;

        public UserRepository(UserManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
       
        public async Task<int> AddAsync(UserEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // Tilføj klubben til DbContext
            await _dbContext.Users.AddAsync(entity);

            // Gem ændringer i databasen
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public Task DeleteAsync(UserEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity?> GetByUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be null or empty", nameof(username));

            // Find brugeren i databasen asynkront
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public Task UpdateAsync(UserEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
