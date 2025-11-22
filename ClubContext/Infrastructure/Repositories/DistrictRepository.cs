using ClubContext.Application.Interfaces;
using ClubContext.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubContext.Infrastructure.Repositories
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly ClubDbContext _dbContext;

        public DistrictRepository(ClubDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<District>> GetAllAsync()
        {
            return await _dbContext.Districts
                 .ToListAsync();
        }

        public Task<District?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
