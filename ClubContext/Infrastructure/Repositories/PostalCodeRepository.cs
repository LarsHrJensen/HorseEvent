using ClubContext.Application.Interfaces;
using ClubContext.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Interfaces.Base;

namespace ClubContext.Infrastructure.Repositories
{
    public class PostalCodeRepository : IPostalCodeRepository
    {
        private readonly ClubDbContext _dbContext;

        public PostalCodeRepository(ClubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        async Task<List<PostalCodeCity>> IReadRepository<PostalCodeCity>.GetAllAsync()
        {
            return await _dbContext.PostalCodeCities
                .AsNoTracking()
                .OrderBy(p => p.PostalCode)
                .ToListAsync();
        }

        async Task<PostalCodeCity?> IReadRepository<PostalCodeCity>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
