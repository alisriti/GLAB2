using System.Data;
using GLab.Domains.Models.Users;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Users.Infra.Storages
{
    public class UserStorage : IUserStorage
    {
        private string connectionString;

        public UserStorage(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("IdentityDB");
        }

        public async Task<User?> SelectUserById(string userId)
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new("select * from USERS where UserId = @aUserId", connection);
            cmd.Parameters.AddWithValue("@aUserId", userId);

            DataTable ds = new();
            SqlDataAdapter da = new(cmd);

            connection.Open();
            da.Fill(ds);

            if (ds.Rows.Count == 0)
                return null;

            return User.Create(
                (string)ds.Rows[0]["UserId"],
                (string)ds.Rows[0]["UserName"],
                (UserState)ds.Rows[0]["State"]
            );
        }

        public async Task<string> SelectUserPassword(string userId)
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new("select Password from USERS where UserId = @aUserId", connection);
            cmd.Parameters.AddWithValue("@aUserName", userId);

            DataTable ds = new();
            SqlDataAdapter da = new(cmd);

            connection.Open();
            da.Fill(ds);

            if (ds.Rows.Count == 0)
                return default;
            return (string)ds.Rows[0]["Password"];
        }

        public async Task<User> SelectUserByUserName(string userName)
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new("select * from USERS where UserName = @aUserName", connection);
            cmd.Parameters.AddWithValue("@aUserName", userName);

            DataTable ds = new();
            SqlDataAdapter da = new(cmd);

            connection.Open();
            da.Fill(ds);

            if (ds.Rows.Count == 0)
                return null;

            return User.Create(
                (string)ds.Rows[0]["UserId"],
                (string)ds.Rows[0]["UserName"],
                (UserState)ds.Rows[0]["State"]
            );
        }
    }
}