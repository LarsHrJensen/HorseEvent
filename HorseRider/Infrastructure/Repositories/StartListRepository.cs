using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HorseRider.Application.DTO_s;
using HorseRider.Application.Interfaces;
using HorseRider.Domain.Entities;
using Microsoft.Data.SqlClient;

public class StartListRepository //: IStartListRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public StartListRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task AddAsync(StartList entity)
    {
        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = @"
            INSERT INTO StartList (StartTime, StartNumber, ClassId, CombinationId)
            VALUES (@StartTime, @StartNumber, @ClassId, @CombinationId);
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@StartTime", entity.StartTime);
        cmd.Parameters.AddWithValue("@StartNumber", entity.StartNumber);
        cmd.Parameters.AddWithValue("@ClassId", entity.ClassId);
        cmd.Parameters.AddWithValue("@CombinationId", entity.CombinationId);

        entity.StartListId = (int)await cmd.ExecuteScalarAsync();
        Console.WriteLine($"Startlist entry {entity.StartListId} oprettet.");
    }

    public async Task DeleteAsync(int id)
    {
        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = "DELETE FROM StartList WHERE StartListId = @StartListId";
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@StartListId", id);

        int rowsAffected = await cmd.ExecuteNonQueryAsync();
        Console.WriteLine(rowsAffected > 0
            ? $"Startlist {id} slettet."
            : $"Ingen startlist fundet med ID {id}.");
    }

    public async Task<StartList?> GetByIdAsync(int id)
    {
        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = "SELECT StartListId, StartTime, StartNumber, ClassId, CombinationId FROM StartList WHERE StartListId = @StartListId";
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@StartListId", id);

        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new StartList
            {
                StartListId = (int)reader["StartListId"],
                StartTime = (DateTime)reader["StartTime"],
                StartNumber = (int)reader["StartNumber"],
                ClassId = (int)reader["ClassId"],
                CombinationId = (int)reader["CombinationId"]
            };
        }

        return null!;
    }

    public async Task<List<StartList>> GetAllAsync()
    {
        var list = new List<StartList>();
        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = "SELECT StartListId, StartTime, StartNumber, ClassId, CombinationId FROM StartList";
        await using var cmd = new SqlCommand(sql, conn);
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(new StartList
            {
                StartListId = (int)reader["StartListId"],
                StartTime = (DateTime)reader["StartTime"],
                StartNumber = (int)reader["StartNumber"],
                ClassId = (int)reader["ClassId"],
                CombinationId = (int)reader["CombinationId"]
            });
        }

        return list;
    }

    public async Task UpdateAsync(StartList entity)
    {
        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = @"
            UPDATE StartList
            SET StartTime = @StartTime,
                StartNumber = @StartNumber,
                ClassId = @ClassId,
                CombinationId = @CombinationId
            WHERE StartListId = @StartListId";

        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@StartListId", entity.StartListId);
        cmd.Parameters.AddWithValue("@StartTime", entity.StartTime);
        cmd.Parameters.AddWithValue("@StartNumber", entity.StartNumber);
        cmd.Parameters.AddWithValue("@ClassId", entity.ClassId);
        cmd.Parameters.AddWithValue("@CombinationId", entity.CombinationId);

        int rowsAffected = await cmd.ExecuteNonQueryAsync();
        Console.WriteLine(rowsAffected > 0
            ? $"Startlist {entity.StartListId} opdateret."
            : $"Ingen startlist fundet med ID {entity.StartListId}.");
    }

    public async Task<List<StartList>> GetByClassIdAsync(int classId)
    {
        var list = new List<StartList>();
        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = "SELECT StartListId, StartTime, StartNumber, ClassId, CombinationId FROM StartList WHERE ClassId = @ClassId";
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ClassId", classId);

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new StartList
            {
                StartListId = (int)reader["StartListId"],
                StartTime = (DateTime)reader["StartTime"],
                StartNumber = (int)reader["StartNumber"],
                ClassId = (int)reader["ClassId"],
                CombinationId = (int)reader["CombinationId"]
            });
        }

        return list;
    }

    public async Task<List<StartListFullDTO>> GetFullStartListByCompetitionAsync(string competitionName)
    {
        var list = new List<StartListFullDTO>();

        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = @"
            SELECT StartlistId, StartNumber, StartTime, RiderName, HorseName, ClassLevel, ProgramName, ClassDate, DisciplineName, CompetitionName

            FROM vw_StartListFull
            WHERE CompetitionName = @CompetitionName
            ORDER BY ClassDate, StartTime";

        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@CompetitionName", competitionName);

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new StartListFullDTO
            {
                StartListId = (int)reader["StartlistId"],
                StartNumber = (int)reader["StartNumber"],
                StartTime = (DateTime)reader["StartTime"],
                RiderName = reader["RiderName"].ToString()! ?? string.Empty,
                HorseName = reader["HorseName"].ToString()! ?? string.Empty,
                ClassLevel = reader["ClassLevel"].ToString()! ?? string.Empty,
                //ProgramName = reader["ProgramName"].ToString()! ?? string.Empty,
                //ClassDate = (DateTime)reader["ClassDate"],
                DisciplineName = reader["DisciplineName"].ToString() ?? string.Empty,
                CompetitionName = reader["CompetitionName"].ToString() ?? string.Empty,
            });
        }

        return list;
    }
}
