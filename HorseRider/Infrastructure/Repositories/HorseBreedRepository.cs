using HorseRiderContext.Application.Interfaces;
using HorseRiderContext.Domain.Entities;
using Microsoft.Data.SqlClient;
using SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRiderContext.Infrastructure.Repositories
{
    public class HorseBreedRepository : IHorseBreedRepository
    {

        private readonly IDbConnectionFactory _dbConnectionFactory;

        public HorseBreedRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<List<HorseBreed>> GetAllAsync()
        {
            var breeds = new List<HorseBreed>();

            await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();

            const string sql = "SELECT BreedID, BreedName FROM HorseBreeds";

            using var cmd = new SqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var breed = new HorseBreed
                {
                    Id = (int)reader["BreedID"],
                    Name = reader["BreedName"].ToString()!
                };

                breeds.Add(breed);
            }

            return breeds;
        }

        public Task<HorseBreed?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
