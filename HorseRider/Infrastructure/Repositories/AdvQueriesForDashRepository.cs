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

        public async Task<IEnumerable<ConsistencyRiderDTO>> GetRiderConsistencyAsync()
        {
            var result = new List<ConsistencyRiderDTO>();

            using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();

            var sql = @"
                SELECT TOP 10
                    r.RiderName,
                    AVG(res.Score) AS AverageScore,
                    CAST(SQRT(VAR(res.Score)) AS DECIMAL(5,2)) AS Consistency
                FROM Result res
                JOIN Combination c ON res.CombinationId = c.CombinationId
                JOIN Rider r ON c.RiderId = r.RiderId
                WHERE res.Score IS NOT NULL
                GROUP BY r.RiderName
                ORDER BY Consistency ASC;
            ";

            using var cmd = new SqlCommand(sql, conn);

            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new ConsistencyRiderDTO
                {
                    RiderName = reader["RiderName"] as string,
                    AverageScore = reader["AverageScore"] as decimal?,
                    Consistency = reader["Consistency"] != DBNull.Value ? Convert.ToDecimal(reader["Consistency"]) : (decimal?)null
                });
            }

            return result;
        }

        public async Task<IEnumerable<RiderHorsePerformanceDTO>> GetRiderHorsePerformanceAsync()
        {
            var result = new List<RiderHorsePerformanceDTO>();

            using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();

            var sql = @"
                SELECT TOP 10
                    r.RiderName,
                    h.HorseName,
                    CAST(AVG(res.Score) AS DECIMAL(5,2)) AS AvgScore,
                    CAST(
                        AVG(res.Score) - (
                            SELECT AVG(res2.Score)
                            FROM Result res2
                            JOIN Combination c2 ON res2.CombinationId = c2.CombinationId
                            WHERE c2.HorseId = c.HorseId
                        ) AS DECIMAL(5,2)
                    ) AS DiffFromHorseAvg
                FROM Result res
                JOIN Combination c ON res.CombinationId = c.CombinationId
                JOIN Rider r ON c.RiderId = r.RiderId
                JOIN Horse h ON c.HorseId = h.HorseId
                WHERE res.Score IS NOT NULL
                GROUP BY r.RiderName, h.HorseName, c.HorseId
                ORDER BY DiffFromHorseAvg DESC;
            ";

            using var cmd = new SqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new RiderHorsePerformanceDTO
                {
                    RiderName = reader["RiderName"].ToString(),
                    HorseName = reader["HorseName"].ToString(),
                    AvgScore = reader["AvgScore"] as decimal?,
                    DiffFromHorseAvg = reader["DiffFromHorseAvg"] as decimal?
                });
            }

            return result;
        }

    }
}
