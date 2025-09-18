using System;
using HorseEvent.Factory;
using HorseEvent.Models;
using Microsoft.Data.SqlClient;


namespace HorseEvent.Repositories
{
    public class HorseRepository : IRepository<Horse> 
    {
        private readonly string _connectionString;

        public HorseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Horse horse)
        {
            using var conn = DbConnectionFactory.GetConnection(_connectionString);
            conn.Open();

            string sql = @"
                           INSERT INTO Horse (HorseUELN, HorseName, Height, BirthYear, Category) 
                           VALUES (@HorseName, @Height, @BirthYear, @Category);
                           SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@HorseUELN", horse.HoreseUELN);
            cmd.Parameters.AddWithValue("@HorseName", horse.HorseName);
            cmd.Parameters.AddWithValue("@Height", horse.Height);
            cmd.Parameters.AddWithValue("@BirthYear", horse.BirthYear);
            cmd.Parameters.AddWithValue("@Category", horse.Category);

            //returnerer den nyoprettede HorseId
            horse.HorseId = (int)cmd.ExecuteScalar();

            cmd.ExecuteNonQuery();
            Console.WriteLine($"Hesten {horse.HorseName} blev oprettet i databasen.");

        }

        public void DeleteByUELN(Horse horse)
        {
            using var conn = DbConnectionFactory.GetConnection(_connectionString);
            conn.Open();

            string sql = "DELETE FROM  Horse WHERE HorseUELN = @HorseUELN";
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@HorseUELN", horse.HoreseUELN);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine($"Hesten med UELN {horse.HoreseUELN} blev slettet fra databasen.");
            }
            else
            {
                Console.WriteLine($"Ingen hest fundet med UELN {horse.HoreseUELN}.");
            }
        }

        public Horse GetById(int id)
        {
            using var conn = DbConnectionFactory.GetConnection(_connectionString);
            conn.Open();

            string sql = "SELECT HorseId, HorseUELN, HorseName, Height, BirthYear, Category FROM Horse WHERE HorseId = @HorseId";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@HorseId", id);

            using var reader = cmd.ExecuteReader();

            if ( reader.Read())
            {
                return new Horse
                {
                    HorseId = (int)reader["HorseId"],
                    HoreseUELN = reader["HorseUELN"].ToString()!,
                    HorseName = reader["HorseName"].ToString()!,
                    Height = (decimal)reader["Height"],
                    BirthYear = (int)reader["BirthYear"],
                    Category = reader["Category"].ToString()!
                };
            }

            return null!;
        }

        public IEnumerable<Horse> GetAll()
        {
            var horses = new List<Horse>();

            using var conn = DbConnectionFactory.GetConnection(_connectionString);
            conn.Open();

            string sql = "SELECT HorseId, HorseUELN, HorseName, Height, BirthYear, Category FROM Horse";

            using var cmd = new SqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read()) 
            {
                var horse = new Horse
                {
                    HorseId = (int)reader["HorseId"],
                    HoreseUELN = reader["HorseUELN"].ToString()!,
                    HorseName = reader["HorseName"].ToString()!,
                    Height = (decimal)reader["Height"],
                    BirthYear = (int)reader["BirthYear"],
                    Category = reader["Category"].ToString()!

                };

                horses.Add(horse);
            }

            return horses;
        }

        public void Update(Horse horse)
        {
            using var conn = DbConnectionFactory.GetConnection(_connectionString);
            conn.Open();

            string sql = @"
                           UPDATE Horse 
                           SET HorseUELN = @HorseUELN, 
                               HorseName = @HorseName, 
                               Height = @Height, 
                               BirthYear = @BirthYear, 
                               Category = @Category
                           WHERE HorseId = @HorseId";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@HorseId", horse.HorseId);
            cmd.Parameters.AddWithValue("@HorseUELN", horse.HoreseUELN);
            cmd.Parameters.AddWithValue("@HorseName", horse.HorseName);
            cmd.Parameters.AddWithValue("@Height", horse.Height);
            cmd.Parameters.AddWithValue("@BirthYear", horse.BirthYear);
            cmd.Parameters.AddWithValue("@Category", horse.Category);
            
            int rowsAffected = cmd.ExecuteNonQuery();
            
            if (rowsAffected > 0) 
                Console.WriteLine($"Hesten med ID {horse.HorseId} blev opdateret.");
            else
                Console.WriteLine($"Ingen hest fundet med ID {horse.HorseId}.");
        }

        public void Delete(int id)
        {
            using var conn = DbConnectionFactory.GetConnection(_connectionString);
            conn.Open();

            string sql = "DELETE FROM  Horse WHERE HorseId = @HorseId";
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@HorseId", id);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
                Console.WriteLine($"Hesten med ID {id} fundet.");
            else
                Console.WriteLine($"Ingen hest fundet med ID {id}.");

        }
    }
}
