using HorseRider.Application.Interfaces;
using HorseRider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRider.Infrastructure.Repositories
{
    public class RiderRepository : IRiderRepository
    {
        private readonly string _connectionString;

        public RiderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task AddAsync(Rider entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Rider entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Rider>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Rider?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Rider entity)
        {
            throw new NotImplementedException();
        }
    }
}
