using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace GLAB.UI.Shared
{
    public partial class PageHeader
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public string SubTitle { get; set; }
    }
}