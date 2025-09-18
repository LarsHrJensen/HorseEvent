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
    public class RiderRepository : IRiderRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public RiderRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task AddAsync(Rider entity)
        {
            await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();

            string sql = @"
                           INSERT INTO Rider (RiderName, BirthDate, MembershipStatus, DRFLicenseNr)
                           VALUES (@RiderName, @BirthDate, @MembershipStatus, @DRFLicenseNr);
                           SELECT CAST(SCOPE_IDENTITY() AS INT);
                           ";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@RiderName", entity.RiderName);
            cmd.Parameters.AddWithValue("@BirthDate", entity.BirthYear);
            cmd.Parameters.AddWithValue("@DRFLicenseNr", entity.DRFLicense ?? (object)DBNull.Value);

            //returnerer den nyoprettede RiderId
            entity.Id = (int)cmd.ExecuteScalar();

            cmd.ExecuteNonQuery();
            Console.WriteLine($"Rytteren {entity.RiderName} blev oprettet i databasen.");
        }

        public Task DeleteAsync(Rider entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Rider>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Rider?> GetByIdAsync(int id)
        {
            await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();

            string sql = "SELECT RiderId, RiderName, BirthDate, MembershipStatus, DRFLicenseNr FROM Rider WHERE RiderId = @RiderId";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@RiderId", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Rider
                {
                    Id = (int)reader["RiderId"],
                    RiderName = reader["RiderName"].ToString()!,
                    BirthYear = ((DateTime)reader["BirthDate"]).Year, // <-- kun året
                    DRFLicense = reader["DRFLicenseNr"].ToString()
                };
            }
            return null!;
        }

        public Task UpdateAsync(Rider entity)
        {
            throw new NotImplementedException();
        }
    }
}
