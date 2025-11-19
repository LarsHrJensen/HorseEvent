using ClubContext.Infrastructure;
using ClubContext.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
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
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task<int> CompleteAsync();
        Task RollbackAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ClubDbContext _context;
        private IDbContextTransaction _transaction;

        public IClubRepository Clubs { get; }
        public IOutboxRepository Outbox { get; }

        public UnitOfWork(ClubDbContext context)
        {
            _context = context;
            Clubs = new ClubRepository(_context);
            Outbox = new OutboxRepository(_context);
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public Task<int> CompleteAsync() => _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();

        
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            await _transaction?.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction?.RollbackAsync();
        }
    }
}
