using System;
using Microsoft.Data.SqlClient;

namespace HorseEvent.Factory
{
    public static class DbConnectionFactory
    {
        public static SqlConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
