using HorseRider.Application.Interfaces;
using HorseRider.Domain.Entities;
using Microsoft.Data.SqlClient;

public class ResultRepository //: IResultRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public ResultRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task AddAsync(Result entity)
    {
        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = @"
            INSERT INTO Result (Score, ResultTime, Faults, Placements, CombinationId, ClassId)
            VALUES (@Score, @ResultTime, @Faults, @Placements, @CombinationId, @ClassId);
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Score", (object?)entity.Score ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@ResultTime", (object?)entity.ResultTime ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Faults", (object?)entity.Faults ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Placements", (object?)entity.Placements ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@CombinationId", entity.CombinationId);
        cmd.Parameters.AddWithValue("@ClassId", entity.ClassId);

        entity.ResultId = (int)await cmd.ExecuteScalarAsync();
    }

    public async Task<Result?> GetByIdAsync(int id)
    {
        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = "SELECT * FROM Result WHERE ResultId = @ResultId";
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ResultId", id);

        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Result
            {
                ResultId = (int)reader["ResultId"],
                Score = reader["Score"] != DBNull.Value ? (decimal)reader["Score"] : null,
                ResultTime = reader["ResultTime"] != DBNull.Value ? (TimeSpan)reader["ResultTime"] : null,
                Faults = reader["Faults"] != DBNull.Value ? (int?)reader["Faults"] : null,
                Placements = reader["Placements"] != DBNull.Value ? (int?)reader["Placements"] : null,
                CombinationId = (int)reader["CombinationId"],
                ClassId = (int)reader["ClassId"]
            };
        }

        return null;
    }

    public async Task<List<Result>> GetAllAsync()
    {
        var results = new List<Result>();

        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = "SELECT * FROM Result";
        await using var cmd = new SqlCommand(sql, conn);
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            results.Add(new Result
            {
                ResultId = (int)reader["ResultId"],
                Score = reader["Score"] != DBNull.Value ? (decimal)reader["Score"] : null,
                ResultTime = reader["ResultTime"] != DBNull.Value ? (TimeSpan)reader["ResultTime"] : null,
                Faults = reader["Faults"] != DBNull.Value ? (int?)reader["Faults"] : null,
                Placements = reader["Placements"] != DBNull.Value ? (int?)reader["Placements"] : null,
                CombinationId = (int)reader["CombinationId"],
                ClassId = (int)reader["ClassId"]
            });
        }

        return results;
    }

    public async Task UpdateAsync(Result entity)
    {
        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = @"
            UPDATE Result
            SET Score = @Score,
                ResultTime = @ResultTime,
                Faults = @Faults,
                Placements = @Placements,
                CombinationId = @CombinationId,
                ClassId = @ClassId
            WHERE ResultId = @ResultId";

        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Score", (object?)entity.Score ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@ResultTime", (object?)entity.ResultTime ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Faults", (object?)entity.Faults ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Placements", (object?)entity.Placements ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@CombinationId", entity.CombinationId);
        cmd.Parameters.AddWithValue("@ClassId", entity.ClassId);
        cmd.Parameters.AddWithValue("@ResultId", entity.ResultId);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = "DELETE FROM Result WHERE ResultId = @ResultId";
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ResultId", id);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<List<Result>> GetByClassIdAsync(int classId)
    {
        var results = new List<Result>();

        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = "SELECT * FROM Result WHERE ClassId = @ClassId";
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ClassId", classId);

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new Result
            {
                ResultId = (int)reader["ResultId"],
                Score = reader["Score"] != DBNull.Value ? (decimal)reader["Score"] : null,
                ResultTime = reader["ResultTime"] != DBNull.Value ? (TimeSpan)reader["ResultTime"] : null,
                Faults = reader["Faults"] != DBNull.Value ? (int?)reader["Faults"] : null,
                Placements = reader["Placements"] != DBNull.Value ? (int?)reader["Placements"] : null,
                CombinationId = (int)reader["CombinationId"],
                ClassId = (int)reader["ClassId"]
            });
        }

        return results;
    }

    public async Task<List<Result>> GetByCombinationIdAsync(int combinationId)
    {
        var results = new List<Result>();

        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = "SELECT * FROM Result WHERE CombinationId = @CombinationId";
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@CombinationId", combinationId);

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new Result
            {
                ResultId = (int)reader["ResultId"],
                Score = reader["Score"] != DBNull.Value ? (decimal)reader["Score"] : null,
                ResultTime = reader["ResultTime"] != DBNull.Value ? (TimeSpan)reader["ResultTime"] : null,
                Faults = reader["Faults"] != DBNull.Value ? (int?)reader["Faults"] : null,
                Placements = reader["Placements"] != DBNull.Value ? (int?)reader["Placements"] : null,
                CombinationId = (int)reader["CombinationId"],
                ClassId = (int)reader["ClassId"]
            });
        }

        return results;
    }

}
