using HorseRider.Application.Interfaces;
using HorseRider.Domain.Entities;
using Microsoft.Data.SqlClient;

public class CombinationRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public CombinationRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task AddAsync(Combination entity)
    {
        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = @"
            INSERT INTO Combination (CombinationStatus, Comment, RiderId, HorseId)
            VALUES (@CombinationStatus, @Comment, @RiderId, @HorseId);
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@CombinationStatus", entity.CombinationStatus);
        cmd.Parameters.AddWithValue("@Comment", entity.Comment ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@RiderId", entity.RiderId);
        cmd.Parameters.AddWithValue("@HorseId", entity.HorseId);

        entity.CombinationId = (int)await cmd.ExecuteScalarAsync();
    }

    public async Task UpdateAsync(Combination entity)
    {
        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = @"
            UPDATE Combination
            SET CombinationStatus = @CombinationStatus,
                Comment = @Comment,
                RiderId = @RiderId,
                HorseId = @HorseId
            WHERE CombinationId = @CombinationId";

        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@CombinationId", entity.CombinationId);
        cmd.Parameters.AddWithValue("@CombinationStatus", entity.CombinationStatus);
        cmd.Parameters.AddWithValue("@Comment", entity.Comment ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@RiderId", entity.RiderId);
        cmd.Parameters.AddWithValue("@HorseId", entity.HorseId);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = "DELETE FROM Combination WHERE CombinationId = @CombinationId";
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@CombinationId", id);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<Combination?> GetByIdAsync(int id)
    {
        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = "SELECT * FROM Combination WHERE CombinationId = @CombinationId";
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@CombinationId", id);

        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Combination
            {
                CombinationId = (int)reader["CombinationId"],
                CombinationStatus = reader["CombinationStatus"].ToString()!,
                Comment = reader["Comment"] != DBNull.Value ? reader["Comment"].ToString() : null,
                RiderId = (int)reader["RiderId"],
                HorseId = (int)reader["HorseId"]
            };
        }

        return null;
    }

    public async Task<List<Combination>> GetAllAsync()
    {
        var combinations = new List<Combination>();

        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = "SELECT * FROM Combination";
        await using var cmd = new SqlCommand(sql, conn);

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            combinations.Add(new Combination
            {
                CombinationId = (int)reader["CombinationId"],
                CombinationStatus = reader["CombinationStatus"].ToString()!,
                Comment = reader["Comment"] != DBNull.Value ? reader["Comment"].ToString() : null,
                RiderId = (int)reader["RiderId"],
                HorseId = (int)reader["HorseId"]
            });
        }

        return combinations;
    }

    public async Task<List<Combination>> GetByHorseIdAsync(int horseId)
    {
        var combinations = new List<Combination>();

        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = "SELECT * FROM Combination WHERE HorseId = @HorseId";
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@HorseId", horseId);

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            combinations.Add(new Combination
            {
                CombinationId = (int)reader["CombinationId"],
                CombinationStatus = reader["CombinationStatus"].ToString()!,
                Comment = reader["Comment"] != DBNull.Value ? reader["Comment"].ToString() : null,
                RiderId = (int)reader["RiderId"],
                HorseId = (int)reader["HorseId"]
            });
        }

        return combinations;
    }

    public async Task<List<Combination>> GetByRiderIdAsync(int riderId)
    {
        var combinations = new List<Combination>();

        await using var conn = (SqlConnection)_dbConnectionFactory.CreateConnection();
        await conn.OpenAsync();

        string sql = "SELECT * FROM Combination WHERE RiderId = @RiderId";
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@RiderId", riderId);

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            combinations.Add(new Combination
            {
                CombinationId = (int)reader["CombinationId"],
                CombinationStatus = reader["CombinationStatus"].ToString()!,
                Comment = reader["Comment"] != DBNull.Value ? reader["Comment"].ToString() : null,
                RiderId = (int)reader["RiderId"],
                HorseId = (int)reader["HorseId"]
            });
        }

        return combinations;
    }
}
