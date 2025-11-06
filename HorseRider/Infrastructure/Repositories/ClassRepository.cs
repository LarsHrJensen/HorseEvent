using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HorseRider.Application.Interfaces;
using HorseRider.Domain.Entities;
using Microsoft.Data.SqlClient;


namespace HorseRider.Infrastructure.Repositories
{
    public class ClassRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public ClassRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        // Hent en klasse ud fra ClassId
        public async Task<Class?> GetByIdAsync(int classId)
        {
            await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();

            string sql = @"
            SELECT ClassId, CompetitionId, DisciplineId, ClassLevel, Height, ProgramName, ClassDate
            FROM Class
            WHERE ClassId = @ClassId";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ClassId", classId);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Class
                {
                    ClassId = (int)reader["ClassId"],
                    CompetitionId = (int)reader["CompetitionId"],
                    DisciplineId = (int)reader["DisciplineId"],
                    ClassLevel = (string)reader["ClassLevel"],
                    Height = reader["Height"] != DBNull.Value ? (int?)reader["Height"] : null,
                    ProgramName = reader["ProgramName"] != DBNull.Value ? (string?)reader["ProgramName"] : null,
                    ClassDate = (DateTime)reader["ClassDate"]
                };
            }

            return null;
        }

        // Hent alle klasser (valgfrit, kan bruges til dropdowns)
        public async Task<List<Class>> GetAllAsync()
        {
            var classes = new List<Class>();

            using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();

            string sql = @"
            SELECT ClassId, CompetitionId, DisciplineId, ClassLevel, Height, ProgramName, ClassDate
            FROM Class";

            using var cmd = new SqlCommand(sql, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                classes.Add(new Class
                {
                    ClassId = (int)reader["ClassId"],
                    CompetitionId = (int)reader["CompetitionId"],
                    DisciplineId = (int)reader["DisciplineId"],
                    ClassLevel = (string)reader["ClassLevel"],
                    Height = reader["Height"] != DBNull.Value ? (int?)reader["Height"] : null,
                    ProgramName = reader["ProgramName"] != DBNull.Value ? (string?)reader["ProgramName"] : null,
                    ClassDate = (DateTime)reader["ClassDate"]
                });
            }

            return classes;
        }
    }
}
