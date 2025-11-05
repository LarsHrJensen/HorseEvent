using EventSchedulingContext.Application.Interfaces;
using EventSchedulingContext.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventSchedulingContext.Infrastructure.Repositories
{
    internal class DisciplinRepository : IDisciplineRepository
    {
        private readonly EventSchedulingDbContext _dbContext;
        public DisciplinRepository(EventSchedulingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Disciplin>> GetAllAsync()
        {
            return await _dbContext.disciplins
                 .ToListAsync();
        }

        public Task<Disciplin?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
