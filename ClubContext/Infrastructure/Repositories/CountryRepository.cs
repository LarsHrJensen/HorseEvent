using ClubContext.Application.Interfaces;
using ClubContext.Domain.ValueObjects;
using ClubContext.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Interfaces.Base;

namespace ClubContext.ClubInfrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ClubDbContext _dbContext;

        public CountryRepository(ClubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Country>> GetAllAsync()
        {
            return await _dbContext.Countries
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Country?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

