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
    public class EventRepository : IEventRepository
    {
        private readonly EventSchedulingDbContext _dbContext;

        public EventRepository(EventSchedulingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Event entity)
        {
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                // Tilføj klubben til DbContext
                await _dbContext.Events.AddAsync(entity);

                // Gem ændringer i databasen
                await _dbContext.SaveChangesAsync();
            }
        }

        public Task DeleteAsync(Event entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Event>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Event?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Event entity)
        {
            throw new NotImplementedException();
        }
    }
}
