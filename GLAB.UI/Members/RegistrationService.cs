using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using GLab.Apps.Members;
using GLab.Apps.Users;
using GLab.Domains.Models.Members;
using GLab.Domains.Models.Users;
using GLAB.UI.Members.Models;

namespace GLAB.UI.Members
{
    public interface IRegistrationService
    {
        Task<bool> RegisterNewMember(CreateMemberModel memberModel);
    }

    public class RegistrationService : IRegistrationService
    {
        private readonly IUserService userService;
        private readonly IMemberService memberService;

        public RegistrationService(IUserService userService, IMemberService memberService)
        {
            this.userService = userService;
            this.memberService = memberService;
        }

        public async Task<bool> RegisterNewMember(CreateMemberModel memberModel)
        {
            using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                string userId = Guid.NewGuid().ToString();

                User userToCreate = User.Create(userId, memberModel.Email, "1122223222");

                Member memberToCreate = new Member()
                {
                    Id = userId,
                    Nom = memberModel.Nom,
                    Prenom = memberModel.Prenom,
                    Email = memberModel.Email,
                    Equipe = memberModel.Equipe
                };

                await userService.CreateUser(userToCreate);
                bool isMemberCreated = await memberService.CreateMember(memberToCreate);

                scope.Complete();
                return isMemberCreated;
            }
            catch (Exception e)
            {
                throw new Exception($"Echec de la création du nouveau membre : {e.Message}");
            }
            finally
            {
                scope.Dispose();
            }
        }
    }
}