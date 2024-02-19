using GLab.Domains.Models.Laboratoires;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLAB.UI.Laboratoires
{
    public partial class ListLabComp
    {
        [Parameter] public List<Laboratoire> Laboratoires { get; set; }
        [Parameter] public EventCallback<string> OnLabSelected { get; set; }

        private void labSelected(string id)
        {
            OnLabSelected.InvokeAsync(id);
        }
    }
}