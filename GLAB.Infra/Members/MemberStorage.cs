using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLab.Domains.Models.Members;
using GLab.Domains.Models.Users;

namespace GLAB.Infra.Members
{
    public class MemberStorage
    {
        private readonly string connectionString;
        private const string insertMemberCommand = "INSERT MEMBERS VALUES(@aMemberId, @aNom, @aPrenom, @aEmail, @aEquipe)";

        public MemberStorage(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<bool> InsertMember(Member member)
        {
            await using var connection = new SqlConnection(connectionString);

            SqlCommand cmd = new(insertMemberCommand, connection);
            cmd.Parameters.AddWithValue("@aMemberId", member.Id);
            cmd.Parameters.AddWithValue("@aNom", member.Nom);
            cmd.Parameters.AddWithValue("@aPrenom", member.Prenom);
            cmd.Parameters.AddWithValue("@aEmail", member.Email);
            cmd.Parameters.AddWithValue("@aEquipe", member.Equipe);

            connection.Open();
            int insertedRows = await cmd.ExecuteNonQueryAsync();
            return (insertedRows > 0);
        }
    }
}