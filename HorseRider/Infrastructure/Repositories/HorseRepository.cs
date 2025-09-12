using HorseRider.Application.Interfaces;
using HorseRider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRider.Infrastructure.Repositories
{
    public class HorseRepository : IHorseRepository
    {
        private readonly string _connectionString;

        public HorseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Task AddAsync(Horse entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Horse entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Horse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Horse?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Horse entity)
        {
            throw new NotImplementedException();
        }
    }
}
