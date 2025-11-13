using HorseEvent.Models;
using HorseEvent.Factory;
using HorseEvent.Models;
using Microsoft.Data.SqlClient;

namespace HorseEvent.Repositories
{
    public class CompititionRepository : IRepository<Competition>
    {
        private readonly string _connectionString;

        public CompititionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Competition competition)
        {
            using var conn = DbConnectionFactory.GetConnection(_connectionString);
            conn.Open();

            string sql = @"
                INSERT INTO Competition (CompetitionName, StartDate, EndDate, Location)
                VALUES (@CompetitionName, @StartDate, @EndDate, @Location);
                SELECT CAST(SCOPE_IDENTITY() AS INT);
             ";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@CompetitionName", competition.CompetitionName);
            cmd.Parameters.AddWithValue("@StartDate", competition.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", competition.EndDate);
            cmd.Parameters.AddWithValue("@Location", competition.Location);
            
            competition.CompetitionId = (int)cmd.ExecuteScalar();

            Console.WriteLine($"Konkurrencen {competition.CompetitionName} blev oprettet i databasen.");
        }

        public IEnumerable<Competition> GetAll()
        {
            var competitions = new List<Competition>();

            using var conn = DbConnectionFactory.GetConnection(_connectionString);
            conn.Open();

            string sql = "SELECT CompetitionId, CompetitionName, StartDate, EndDate, Location FROM Competition";

            using var cmd = new SqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                competitions.Add(new Competition
                {
                    CompetitionId = (int)reader["CompetitionId"],
                    CompetitionName = reader["CompetitionName"].ToString()!,
                    StartDate = (DateTime)reader["StartDate"],
                    EndDate = (DateTime)reader["EndDate"],
                    Location = reader["Location"].ToString()!
                });
            }

            return competitions;
        }

        public Competition GetById(int id)
        {
            using var conn = DbConnectionFactory.GetConnection(_connectionString);
            conn.Open();

            string sql = "SELECT CompetitionId, CompetitionName, StartDate, EndDate, Location FROM Competition WHERE CompetitionId = @CompetitionId";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@CompetitionId", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Competition
                {
                    CompetitionId = (int)reader["CompetitionId"],
                    CompetitionName = reader["CompetitionName"].ToString()!,
                    StartDate = (DateTime)reader["StartDate"],
                    EndDate = (DateTime)reader["EndDate"],
                    Location = reader["Location"].ToString()!
                };
            }

            return null;
        }

        public Competition GetByCompetitionId(int id)
        {
            using var conn = DbConnectionFactory.GetConnection(_connectionString);
            conn.Open();

            string compSql = "SELECT CompetitionId, CompetitionName, StartDate, EndDate, Location FROM Competition WHERE CompetitionId = @CompetitionId";
            using var compCmd = new SqlCommand(compSql, conn);
            compCmd.Parameters.AddWithValue("@CompetitionId", id);

            Competition competition = null!;

            using (var reader = compCmd.ExecuteReader())
            {
                if (reader.Read())
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

            if (competition == null)
                return null!;

            //Henter alle klasser til konkurrencen
            string classSql = "SELECT ClassId, ClassName, DifficultyLevel, MaxHeight FROM Class WHERE CompetitionId = @CompetitionId";
            using var classCmd = new SqlCommand(classSql, conn);
            classCmd.Parameters.AddWithValue("@CompetitionId", id);

            using var classReader = classCmd.ExecuteReader();
            while (classReader.Read())
            {
                competition.Classes.Add(new Class
                {
                    ClassId = (int)classReader["ClassId"],
                    DisciplineId = (int)classReader["DisciplineId"],
                    ClassLevel = classReader["ClassLevel"].ToString()!,
                    Height = classReader["Height"] != DBNull.Value ? (decimal?)classReader["Height"] : 0,
                    ProgramName = classReader["ProgramName"].ToString()!,
                    ClassDate = (DateTime)classReader["ClassDate"]
                });
            }
            return competition;
        }

        public void Update(Competition competition)
        {
            using var conn = DbConnectionFactory.GetConnection(_connectionString);
            conn.Open();

            string sql = @"
                UPDATE Competition
                SET CompetitionName = @CompetitionName,
                    StartDate = @StartDate,
                    EndDate = @EndDate,
                    Location = @Location
                WHERE CompetitionId = @CompetitionId";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@CompetitionId", competition.CompetitionId);
            cmd.Parameters.AddWithValue("@CompetitionName", competition.CompetitionName);
            cmd.Parameters.AddWithValue("@StartDate", competition.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", competition.EndDate);
            cmd.Parameters.AddWithValue("@Location", competition.Location);
            
            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine(rowsAffected > 0
                ? $"Konkurrencen med ID {competition.CompetitionId} blev opdateret."
                : $"Ingen konkurrencer blev opdateret. Tjek om konkurrencen med ID {competition.CompetitionId} findes.");
        }

        public void Delete(int id)
        {
            using var conn = DbConnectionFactory.GetConnection(_connectionString);
            conn.Open();

            string sql = "DELETE FROM Competition WHERE CompetitionId = @CompetitionId";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@CompetitionId", id);

            int rowsAffected = cmd.ExecuteNonQuery();
            Console.WriteLine(rowsAffected > 0
                ? $"Konkurrencen med ID {id} blev slettet."
                : $"Ingen konkurrencer blev slettet. Tjek om konkurrencen med ID {id} findes.");
        }
    }
}
