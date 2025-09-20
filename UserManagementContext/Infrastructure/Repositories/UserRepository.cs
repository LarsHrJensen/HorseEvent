using UserManagementContext.Application.Interfaces;
using UserManagementContext.Domain.Entities;

namespace UserManagementContext.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task AddAsync(UserEntity entity)
        {
            throw new NotImplementedException();
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
