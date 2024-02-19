using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace GLAB.UI.Shared
{
    public partial class MyCard
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public bool ShowFooter { get; set; } = true;
        [Parameter] public string CancelLabel { get; set; } = "Annuler";
        [Parameter] public string SubmitLabel { get; set; } = "Ok";
        [Parameter] public RenderFragment CardBody { get; set; }
    }
}