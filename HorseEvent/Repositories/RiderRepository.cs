using HorseEvent.Models;
using HorseEvent.Factory;
using HorseEvent.Models;
using Microsoft.Data.SqlClient;

namespace HorseEvent.Repositories
{
    public class RiderRepository : IRepository<Rider>
    {
        private readonly string _connectionString;

        public RiderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Rider rider)
        {
            using var conn = DbConnectionFactory.GetConnection(_connectionString);
            conn.Open();

            string sql = @"
                           INSERT INTO Rider (RiderName, BirthDate, MembershipStatus, DRFLicenseNr)
                           VALUES (@RiderName, @BirthDate, @MembershipStatus, @DRFLicenseNr);
                           SELECT CAST(SCOPE_IDENTITY() AS INT);
                           ";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@RiderName", rider.RiderName);
            cmd.Parameters.AddWithValue("@BirthDate", rider.BirthDate);
            cmd.Parameters.AddWithValue("@MembershipStatus", rider.MembershipStatus);
            cmd.Parameters.AddWithValue("@DRFLicenseNr", rider.DRFLicenseNr ?? (object)DBNull.Value);

            //returnerer den nyoprettede RiderId
            rider.RiderId = (int)cmd.ExecuteScalar();

            cmd.ExecuteNonQuery();
            Console.WriteLine($"Rytteren {rider.RiderName} blev oprettet i databasen.");
        }

        public Rider GetById(int id)
        {
            using var conn = DbConnectionFactory.GetConnection(_connectionString);
            conn.Open();

            string sql = "SELECT RiderId, RiderName, BirthDate, MembershipStatus, DRFLicenseNr FROM Rider WHERE RiderId = @RiderId";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@RiderId", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Rider
                {
                    RiderId = (int)reader["RiderId"],
                    RiderName = reader["RiderName"].ToString()!,
                    BirthDate = (DateTime)reader["BirthDate"],
                    MembershipStatus = (bool)reader["MembershipStatus"],
                    DRFLicenseNr = reader["DRFLicenseNr"].ToString()
                };
            }

            return null!;
        }
    }
}
