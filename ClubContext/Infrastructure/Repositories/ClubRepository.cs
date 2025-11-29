using ClubContext.Application.Interfaces;
using ClubContext.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Interfaces.Base;

namespace ClubContext.Infrastructure.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private readonly ClubDbContext _dbContext;

        public ClubRepository(ClubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Club> AddAsync(Club entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

           var newClub = await _dbContext.Clubs.AddAsync(entity);

            return newClub.Entity;    // INGEN SaveChanges her
        }

        public async Task<List<Club>> GetAllAsync()
        {
            return await _dbContext.Clubs.ToListAsync();
        }


        public Task DeleteAsync(Club entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Club?> GetByIdAsync(int id)
        {
            return await _dbContext.Clubs
                               .FirstOrDefaultAsync(c => c.ClubId == id);
        }

        public Task UpdateAsync(Club entity)
        {
            throw new NotImplementedException();
        }
    }
}
