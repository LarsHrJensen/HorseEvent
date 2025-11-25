using Npgsql;
using System.Text;
using System.Text.Json;

namespace OutboxWatcher
{
    public class OutboxProcessor
    {
        private readonly string _connectionString;
        private readonly HttpClient _httpClient;

        public OutboxProcessor(string connectionString)
        {
            _connectionString = connectionString;
            _httpClient = new HttpClient();
        }

        public async Task RunAsync()
        {
            var items = LoadPendingEvents();

            foreach (var item in items)
            {
                bool ok = await SendToApiAsync(item.Payload);

                if (ok)
                {
                    MarkAsProcessed(item.Id);
                }
            }
        }

        private List<OutboxItem> LoadPendingEvents()
        {
            var result = new List<OutboxItem>();

            string sql = @"SELECT ""Id"", ""EventType"", ""Payload"", ""CreatedAt"" 
                       FROM ""Outbox"" 
                       WHERE ""Processed"" = false";

            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new OutboxItem
                {
                    Id = reader.GetInt32(0),
                    EventType = reader.GetString(1),
                    Payload = reader.GetString(2),
                    CreatedAt = reader.GetDateTime(3),
                    Processed = false
                });
            }

            return result;
        }

        private async Task<bool> SendToApiAsync(string payload)
        {
            try
            {
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7265/api/clubintegration/created", content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        private void MarkAsProcessed(int id)
        {
            string sql = @"UPDATE ""Outbox"" SET ""Processed"" = true WHERE ""Id"" = @id";

            // VIGTIGT: NY connection → ingen “command already in progress”
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }
    }

}
