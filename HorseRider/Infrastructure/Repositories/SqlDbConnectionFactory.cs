using Microsoft.Data.SqlClient;
using SharedKernel.Interfaces;
using System.Data;

namespace HorseRider.Infrastructure.Repositories
{
    public class SqlDbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public SqlDbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
