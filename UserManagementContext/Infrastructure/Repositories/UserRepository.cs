using ClubContext.Domain.Entities;
using ClubContext.Infrastructure;
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
       
        public async Task AddAsync(UserEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // Tilføj klubben til DbContext
            await _dbContext.Users.AddAsync(entity);

            // Gem ændringer i databasen
            await _dbContext.SaveChangesAsync();
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

        public Task UpdateAsync(UserEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
