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

        public async Task<int> AddAsync(Club entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

           var newClub = await _dbContext.Clubs.AddAsync(entity);

            return newClub.Entity.ClubId;    // INGEN SaveChanges her
        }

        public async Task<List<Club>> GetAllAsync()
        {
            return await _dbContext.Clubs.ToListAsync();
        }


        public Task DeleteAsync(Club entity)
        {
            throw new NotImplementedException();
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
