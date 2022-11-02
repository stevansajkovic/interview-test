using System;
using System.Data.SqlClient;
using TicketManagementSystem.Models;
using TicketManagementSystem.Repositories.Interfaces;

namespace TicketManagementSystem.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository()
        {

        }

        public User GetUser(string username)
        {
            // Assume this method does not need to change and is connected to a database with users populated.
            try
            {
                using var connection = GetConnection();

                string sql = "SELECT TOP 1 FROM Users u WHERE u.Username == @p1";
                connection.Open();

                var command = new SqlCommand(sql)
                {
                    CommandType = System.Data.CommandType.Text,
                };

                command.Parameters.Add("@p1", System.Data.SqlDbType.NVarChar).Value = username;

                return (User)command.ExecuteScalar();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User GetAccountManager()
        {
            // Assume this method does not need to change.
            return GetUser("Sarah");
        }

        public override void Dispose()
        {
            // Assume this method does not need to change.
            base.Dispose();
        }
    }
}
