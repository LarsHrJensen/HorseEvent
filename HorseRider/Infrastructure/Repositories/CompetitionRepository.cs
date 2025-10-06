using HorseRider.Application.Interfaces;
using HorseRider.Domain.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HorseRider.Infrastructure.Repositories
{
    public class CompetitionRepository //: ICompetitionRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public CompetitionRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task AddAsync(Competition competition)
        {
            await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();

            string sql = @"
                INSERT INTO Competition (CompetitionName, StartDate, EndDate, Location)
                VALUES (@CompetitionName, @StartDate, @EndDate, @Location);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@CompetitionName", competition.CompetitionName);
            cmd.Parameters.AddWithValue("@StartDate", competition.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", competition.EndDate);
            cmd.Parameters.AddWithValue("@Location", competition.Location);

            competition.CompetitionId = (int)await cmd.ExecuteScalarAsync();
            Console.WriteLine($"Konkurrencen {competition.CompetitionName} blev oprettet med ID {competition.CompetitionId}.");
        }

        public async Task<Competition?> GetByIdAsync(int id)
        {
            Competition? competition = null;

            await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();

            string sql = "SELECT CompetitionId, CompetitionName, StartDate, EndDate, Location FROM Competition WHERE CompetitionId = @CompetitionId";
            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@CompetitionId", id);

            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    competition = new Competition
                    {
                        CompetitionId = (int)reader["CompetitionId"],
                        CompetitionName = reader["CompetitionName"].ToString()!,
                        StartDate = (DateTime)reader["StartDate"],
                        EndDate = (DateTime)reader["EndDate"],
                        Location = reader["Location"].ToString()!,
                        Classes = new List<Class>()
                    };
                }
            }

            if (competition == null) return null!;

            // Hent tilhørende klasser
            string classSql = "SELECT ClassId, DisciplineId, ClassLevel, Height, ProgramName, ClassDate FROM Class WHERE CompetitionId = @CompetitionId";
            await using var classCmd = new SqlCommand(classSql, conn);
            classCmd.Parameters.AddWithValue("@CompetitionId", id);

            await using var classReader = await classCmd.ExecuteReaderAsync();
            while (await classReader.ReadAsync())
            {
                competition.Classes.Add(new Class
                {
                    ClassId = (int)classReader["ClassId"],
                    DisciplineId = (int)classReader["DisciplineId"],
                    ClassLevel = classReader["ClassLevel"].ToString()!,
                    Height = classReader["Height"] != DBNull.Value ? Convert.ToInt32(classReader["Height"]) : 0,
                    ProgramName = classReader["ProgramName"].ToString()!,
                    ClassDate = (DateTime)classReader["ClassDate"]
                });
            }

            return competition;
        }

        public async Task<List<Competition>> GetAllAsync()
        {
            var competitions = new List<Competition>();

            await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();

            string sql = "SELECT CompetitionId, CompetitionName, StartDate, EndDate, Location FROM Competition";
            await using var cmd = new SqlCommand(sql, conn);

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                competitions.Add(new Competition
                {
                    CompetitionId = (int)reader["CompetitionId"],
                    CompetitionName = reader["CompetitionName"].ToString()!,
                    StartDate = (DateTime)reader["StartDate"],
                    EndDate = (DateTime)reader["EndDate"],
                    Location = reader["Location"].ToString()!,
                    Classes = new List<Class>()
                });
            }

            return competitions;
        }

        public async Task UpdateAsync(Competition competition)
        {
            await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();

            string sql = @"
                UPDATE Competition
                SET CompetitionName = @CompetitionName,
                    StartDate = @StartDate,
                    EndDate = @EndDate,
                    Location = @Location
                WHERE CompetitionId = @CompetitionId";

            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@CompetitionId", competition.CompetitionId);
            cmd.Parameters.AddWithValue("@CompetitionName", competition.CompetitionName);
            cmd.Parameters.AddWithValue("@StartDate", competition.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", competition.EndDate);
            cmd.Parameters.AddWithValue("@Location", competition.Location);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            Console.WriteLine(rowsAffected > 0
                ? $"Konkurrencen med ID {competition.CompetitionId} blev opdateret."
                : $"Ingen konkurrence fundet med ID {competition.CompetitionId}.");
        }

        public async Task DeleteAsync(int id)
        {
            await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
            await conn.OpenAsync();

            string sql = "DELETE FROM Competition WHERE CompetitionId = @CompetitionId";
            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@CompetitionId", id);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            Console.WriteLine(rowsAffected > 0
                ? $"Konkurrencen med ID {id} blev slettet."
                : $"Ingen konkurrence fundet med ID {id}.");
        }
    }
}

