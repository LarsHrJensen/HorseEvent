using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HorseRider.Domain.Entities;
using Microsoft.Data.SqlClient;
using SharedKernel.Interfaces;
using HorseRiderContext.Application.DTO_s;

namespace HorseRiderContext.Infrastructure.Repositories
{
    public class RiderPerformanceRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public RiderPerformanceRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<List<RiderPerformanceItem>> GetByRiderDrfNrAsync(string DRFLicenseNr)
        {
            var performanceList = new List<RiderPerformanceItem>();

            await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();

            string sql = @"
                SELECT *
                FROM RiderPerformance
                WHERE DRFLicenseNr = @DRFLicenseNr
                ORDER BY CompetitionStart, CLassDate, StartTime";

            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@DRFLicenseNr", DRFLicenseNr);

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                performanceList.Add(new RiderPerformanceItem
                {
                    RiderId = (int)reader["RiderId"],
                    RiderName = reader["RiderName"] as string,
                    DRFLicenseNr = reader["DRFLicenseNr"] as string,

                    HorseId = (int)reader["HorseId"],
                    HorseName = reader["HorseName"] as string,

                    CompetitionId = (int)reader["CompetitionId"],
                    CompetitionName = reader["CompetitionName"] as string,
                    CompetitionStart = (DateTime)reader["CompetitionStart"],
                    CompetitionEnd = (DateTime)reader["CompetitionEnd"],

                    ClassId = (int)reader["ClassId"],
                    ClassLevel = reader["ClassLevel"] as string,
                    ClassDate = (DateTime)reader["ClassDate"],
                    ProgramName = reader["ProgramName"] as string,
                    Height = reader["Height"] as int?,

                    DisciplineId = (int)reader["DisciplineId"],
                    DisciplineName = reader["DisciplineName"] as string,

                    StartListId = reader["StartListId"] as int?,
                    StartNumber = reader["StartNumber"] as int?,
                    StartTime = reader["StartTime"] as DateTime?,

                    ResultId = reader["ResultId"] as int?,
                    Score = reader["Score"] as decimal?,
                    Faults = reader["Faults"] as int?,
                    ResultTime = reader["ResultTime"] as TimeSpan?,
                    Placements = reader["Placements"] as int?
                });
            }
            return performanceList;
        }
    }
}
