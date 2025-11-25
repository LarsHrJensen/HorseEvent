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

        public IEnumerable<Rider> GetAll()
        {
            var riders = new List<Rider>();

            using var conn = DbConnectionFactory.GetConnection(_connectionString);
            conn.Open();

            string sql = "SELECT RiderId, RiderName, BirthDate, MembershipStatus, DRFLicenseNr FROM Rider";

            using var cmd = new SqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                riders.Add(new Rider
                {
                    RiderId = (int)reader["RiderId"],
                    RiderName = reader["RiderName"].ToString()!,
                    BirthDate = (DateTime)reader["BirthDate"],
                    MembershipStatus = (bool)reader["MembershipStatus"],
                    DRFLicenseNr = reader["DRFLicenseNr"].ToString()
                });
            }

            return riders;
        }

        public void Update (Rider rider)
        {
            using var conn = DbConnectionFactory.GetConnection(_connectionString);
            conn.Open();

            string sql = @"
                   UPDATE Rider
                   SET RiderName = @RiderName,
                       BirthDate = @Birthdate,
                       MembershipStatus = @MembershipStatus,
                       DRFLicenseNr = @DRFLicenseNr
                   WHERE RiderId = @RiderId
             ";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@RiderId", rider.RiderId);
            cmd.Parameters.AddWithValue("@RiderName", rider.RiderName);
            cmd.Parameters.AddWithValue("@BirthDate", rider.BirthDate);
            cmd.Parameters.AddWithValue("@MembershipStatus", rider.MembershipStatus);
            cmd.Parameters.AddWithValue("@DRFLicenseNr", rider.DRFLicenseNr ?? (object)DBNull.Value);

            int rowsAffected = cmd.ExecuteNonQuery();

            Console.WriteLine(rowsAffected > 0
                ? $"Rytteren med ID {rider.RiderId} blev opdateret."
                : $"Ingen rytter fundet med ID {rider.RiderId},");
        }

        public void Delete(int id)
        {
            using var conn = DbConnectionFactory.GetConnection(_connectionString);
            conn.Open();

            string sql = "DELETE FROM Rider WHERE RiderId = @RiderId";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@RiderId", id);

            int rowsAffected = cmd.ExecuteNonQuery();

            Console.WriteLine(rowsAffected > 0
                ? $"Rytteren med ID {id} blev slettet."
                : $"Ingen rytter fundet med ID {id}, så ingen sletning skete.");
        }
    }
}
