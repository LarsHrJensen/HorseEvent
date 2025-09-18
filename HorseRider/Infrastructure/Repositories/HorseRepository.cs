using HorseRider.Application.Interfaces;
using HorseRider.Domain.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRider.Infrastructure.Repositories
{
    public class HorseRepository : IHorseRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public HorseRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task AddAsync(Horse entity)
        {
            // Cast til SqlConnection
            await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();

            string sql = @"INSERT INTO Horse (HorseName, Height, BirthYear, UELN) 
                       VALUES (@HorseName, @Height, @BirthYear, @UELN)";

            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@HorseName", entity.Name);
            cmd.Parameters.AddWithValue("@Height", entity.Height);
            cmd.Parameters.AddWithValue("@BirthYear", entity.BirthYear);
            cmd.Parameters.AddWithValue("@UELN", entity.UELN);

            await cmd.ExecuteNonQueryAsync();

            Console.WriteLine($"Hesten {entity.Name} blev oprettet i databasen.");
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
