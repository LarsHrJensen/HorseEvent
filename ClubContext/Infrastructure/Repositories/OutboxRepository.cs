using ClubContext.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubContext.Infrastructure.Repositories
{
    internal class OutboxRepository : IOutboxRepository
    {

        public ClubDbContext Context { get; }
        public OutboxRepository(ClubDbContext context)
        {
            Context = context;
        }

        public async  Task <OutboxEvent>AddAsync(OutboxEvent entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var newClub = await Context.Outbox.AddAsync(entity);

            return newClub.Entity;    // INGEN SaveChanges her
        }

     
    }
}
