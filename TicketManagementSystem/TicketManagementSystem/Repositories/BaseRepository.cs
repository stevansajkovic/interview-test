using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TicketManagementSystem.Repositories
{
    public class BaseRepository : IDisposable
    {
        private static string _connectionString => ConfigurationManager.ConnectionStrings["database"].ConnectionString;

        protected static IDbConnection GetConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            return connection;
        }

        public virtual void Dispose()
        {

        }
    }
}
