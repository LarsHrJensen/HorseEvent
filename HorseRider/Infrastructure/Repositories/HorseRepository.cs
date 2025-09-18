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

        public async Task DeleteAsync(Horse entity)
        {
            await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();

            string sql = "DELETE FROM  Horse WHERE HorseUELN = @HorseUELN";
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@HorseUELN", entity.UELN);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine($"Hesten med UELN {entity.UELN} blev slettet fra databasen.");
            }
            else
            {
                Console.WriteLine($"Ingen hest fundet med UELN {entity.UELN}.");
            }
        }

        public async Task<List<Horse>> GetAllAsync()
        {
            var horses = new List<Horse>();

            await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();

            string sql = "SELECT HorseId, UELN, HorseName, Height, BirthYear FROM Horse";

            using var cmd = new SqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var horse = new Horse
                {
                    HorseId = (int)reader["HorseId"],
                    UELN = reader["UELN"].ToString()!,
                    Name = reader["HorseName"].ToString()!,
                    Height = (int)(decimal)reader["Height"],
                    BirthYear = (int)reader["BirthYear"],
                };

                horses.Add(horse);
            }

            return horses;
        }

        public async Task<Horse?> GetByIdAsync(int id)
        {
            await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();

            string sql = "SELECT HorseId, HorseUELN, HorseName, Height, BirthYear, Category FROM Horse WHERE HorseId = @HorseId";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@HorseId", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Horse
                {
                    HorseId = (int)reader["HorseId"],
                    UELN = reader["HorseUELN"].ToString()!,
                    Name = reader["HorseName"].ToString()!,
                    Height = (int)(decimal)reader["Height"],
                    BirthYear = (int)reader["BirthYear"],
                };
            }

            return null!;
        }
        

        public async Task UpdateAsync(Horse entity)
        {
            await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();


            string sql = @"
                           UPDATE Horse 
                           SET HorseUELN = @HorseUELN, 
                               HorseName = @HorseName, 
                               Height = @Height, 
                               BirthYear = @BirthYear, 
                               Category = @Category
                           WHERE HorseId = @HorseId";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@HorseId", entity.HorseId);
            cmd.Parameters.AddWithValue("@HorseUELN", entity.UELN);
            cmd.Parameters.AddWithValue("@HorseName", entity.Name);
            cmd.Parameters.AddWithValue("@Height", entity.Height);
            cmd.Parameters.AddWithValue("@BirthYear", entity.BirthYear);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
                Console.WriteLine($"Hesten med ID {entity.HorseId} blev opdateret.");
            else
                Console.WriteLine($"Ingen hest fundet med ID {entity.HorseId}.");
        }
    }
}
