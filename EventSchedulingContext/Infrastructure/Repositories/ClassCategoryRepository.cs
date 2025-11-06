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
    public class ClassCategoryRepository : IClassCategoryRepository
    {
        private readonly EventSchedulingDbContext _dbContext;

        public ClassCategoryRepository(EventSchedulingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<ClassCategory>> GetAllAsync()
        {
            return await _dbContext.classCategories
                 .ToListAsync();
        }

        public Task<ClassCategory?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
