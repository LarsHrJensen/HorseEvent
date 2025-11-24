using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HorseRiderContext.Application.DTO_s;
using SharedKernel.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HorseRiderContext.Infrastructure.Repositories
{
    public class AdvQueriesForDashRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public AdvQueriesForDashRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<TopRiderDTO>> GetTopRidersAsync(int year, int topN)
        {
            var result = new List<TopRiderDTO>();

            using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();

            var sql = $@"
            SELECT TOP (@TopN)
                   r.RiderName,
                   AVG(p.Score) AS AverageScore,
                   SUM(p.Score) AS TotalScore,
                   COUNT(*) AS Competitions
            FROM RiderPerformance p
            JOIN Rider r ON p.RiderId = r.RiderId
            WHERE YEAR(p.StartTime) = @Year
            GROUP BY r.RiderName
            ORDER BY AverageScore DESC;
        
            ";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Year", year);
            cmd.Parameters.AddWithValue("@TopN", topN);

            using var reader = await cmd.ExecuteReaderAsync();   

            while (await reader.ReadAsync())
            {
                result.Add(new TopRiderDTO
                {
                    RiderName = (string)reader["RiderName"],
                    AverageScore = reader["AverageScore"] as decimal?,
                    TotalScore = reader["TotalScore"] as decimal?,
                    Competitions = (int)reader["Competitions"]
                });
            }

            return result;
        }
    }
}
