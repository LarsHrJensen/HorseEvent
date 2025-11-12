using ClubContext.Application.Interfaces;
using ClubContext.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubContext.Infrastructure.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private readonly ClubDbContext _dbContext;

        public ClubRepository(ClubDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddAsync(Club entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // Tilføj klubben til DbContext
            await _dbContext.Clubs.AddAsync(entity);

            // Gem ændringer i databasen
            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public Task DeleteAsync(Club entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Club>> GetAllAsync()
        {
            return await _dbContext.Clubs.ToListAsync();
        }

        public Task<Club?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Club entity)
        {
            throw new NotImplementedException();
        }
    }
}
