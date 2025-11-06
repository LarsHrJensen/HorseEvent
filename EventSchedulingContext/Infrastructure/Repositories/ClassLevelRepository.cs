using EventSchedulingContext.Application.Interfaces;
using EventSchedulingContext.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSchedulingContext.Infrastructure.Repositories
{
    public class ClassLevelRepository : IClassLevelRepository
    {
        private readonly EventSchedulingDbContext _dbContext;

        public ClassLevelRepository(EventSchedulingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<ClassLevel>> GetAllAsync()
        {
            return await _dbContext.ClassLevels
                 .ToListAsync();
        }

        public Task<ClassLevel?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
