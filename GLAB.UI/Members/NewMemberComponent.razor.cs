using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLAB.UI.Members.Models;
using Microsoft.AspNetCore.Components;

namespace GLAB.UI.Members
{
    public partial class NewMemberComponent
    {
        [Inject] private IRegistrationService registrationService { get; set; }

        private CreateMemberModel memberModel = new CreateMemberModel();

        private bool hasError = false;
        private string errorMessage = string.Empty;

        private async Task createMember()
        {
            await registrationService.RegisterNewMember(memberModel);
        }
    }
}