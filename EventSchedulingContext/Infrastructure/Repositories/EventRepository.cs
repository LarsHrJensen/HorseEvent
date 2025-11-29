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
        public async Task<Event> AddAsync(Event entity)
        {
            
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // Tilføj klubben til DbContext
            await _dbContext.Events.AddAsync(entity);

            // Gem ændringer i databasen
            await _dbContext.SaveChangesAsync();
            
            return entity;
        }

        public Task DeleteAsync(Event entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await _dbContext.Events
                  .ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _dbContext.Events
                .Include(e => e.Classes) // Hent alle klasser til stævnet
                .FirstOrDefaultAsync(e => e.Id == id); // Hent kun stævnet med det givne id
        }

        public Task UpdateAsync(Event entity)
        {
            throw new NotImplementedException();
        }
    }
}
