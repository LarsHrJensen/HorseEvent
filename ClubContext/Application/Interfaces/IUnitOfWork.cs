using ClubContext.Infrastructure;
using ClubContext.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubContext.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IClubRepository Clubs { get; }
        IOutboxRepository Outbox { get; }
        Task<int> CompleteAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ClubDbContext _context;

        public IClubRepository Clubs { get; }
        public IOutboxRepository Outbox { get; }

        public UnitOfWork(ClubDbContext context)
        {
            _context = context;
            Clubs = new ClubRepository(_context);
            Outbox = new OutboxRepository(_context);
        }

        public Task<int> CompleteAsync() => _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
