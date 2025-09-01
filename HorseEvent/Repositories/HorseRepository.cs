using System;
using HorseEvent.Factory;
using HorseEvent.Models;
using Microsoft.Data.SqlClient;


namespace HorseEvent.Repositories
{
    public class HorseRepository //: IRepository<Horse> (interface implementeres senere)
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

            string sql = @"INSERT INTO Horse (HorseName, Height, BirthYear, Category) 
                           VALUES (@HorseName, @Height, @BirthYear, @Category)";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@HorseName", horse.HorseName);
            cmd.Parameters.AddWithValue("@Height", horse.Height);
            cmd.Parameters.AddWithValue("@BirthYear", horse.BirthYear);
            cmd.Parameters.AddWithValue("@Category", horse.Category);

            cmd.ExecuteNonQuery();
            Console.WriteLine($"Hesten {horse.HorseName} blev oprettet i databasen.");

        }
    }
}
