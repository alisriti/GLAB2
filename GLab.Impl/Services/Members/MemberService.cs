using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLab.Apps.Members;
using GLab.Domains.Models.Members;
using GLab.Domains.Models.Users;
using GLAB.Infra.Members;
using Microsoft.Extensions.Configuration;

namespace GLab.Impl.Services.Members
{
    public class MemberService : IMemberService
    {
        private MemberStorage memberStorage;

        public MemberService(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("GLabDB");
            memberStorage = new MemberStorage(connectionString);
        }

        public async Task<bool> CreateMember(Member member)
        {
            validateMemberForCreation(member);
            validateMemberTeam(member);

            return await memberStorage.InsertMember(member);
        }

        private void validateMemberTeam(Member member)
        {
        }

        private void validateMemberForCreation(Member member)
        {
            if (member is null)
                throw new Exception("Aucun membre n'est fourni");

            if (string.IsNullOrWhiteSpace(member.Id) ||
                string.IsNullOrWhiteSpace(member.Nom) ||
                string.IsNullOrWhiteSpace(member.Prenom) ||
                string.IsNullOrWhiteSpace(member.Email))
                throw new Exception("Erreur de validation du membre");
        }
    }
}