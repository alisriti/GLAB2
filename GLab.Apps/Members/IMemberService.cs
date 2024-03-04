using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLab.Domains.Models.Members;

namespace GLab.Apps.Members
{
    public interface IMemberService
    {
        Task<bool> CreateMember(Member member);
    }
}