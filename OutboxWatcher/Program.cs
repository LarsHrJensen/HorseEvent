using System;
using Npgsql;
using System.Threading;

namespace OutboxWatcher
{
    class Program
    {
        private static readonly int interval = 10000; // 10 sekunder

        static void Main(string[] args)
        {
            Console.WriteLine("Starter overvågning af Outbox (PostgreSQL)...");

            Timer timer = new Timer(CheckOutbox, null, 0, interval);

            Console.WriteLine("Tryk på Enter for at afslutte.");
            Console.ReadLine();
        }

        private static void CheckOutbox(object state)
        {
            string connectionString = "Host=116.203.199.232;Port=5432;Database=ClubDB;Username=Ea;Password=EaErSjov";
            string query = "SELECT \"Id\", \"EventType\", \"Payload\", \"CreatedAt\" FROM \"Outbox\" WHERE \"Processed\" = false";

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string eventType = reader.GetString(1);
                            string payload = reader.GetString(2);
                            DateTime createdAt = reader.GetDateTime(3);

                            Console.WriteLine($"Ny besked: Id={id}, EventType={eventType}, Payload={payload}, CreatedAt={createdAt}");

                            // Marker beskeden som processeret
                            //MarkAsProcessed(id, connection);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl under forespørgsel: {ex.Message}");
            }
        }

        private static void MarkAsProcessed(int id, NpgsqlConnection connection)
        {
            string updateQuery = "UPDATE Outbox SET Processed = true WHERE Id = @Id";
            using (var command = new NpgsqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
