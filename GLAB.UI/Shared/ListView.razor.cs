using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLAB.UI.Shared
{
    public partial class ListView<TItem> : ComponentBase
    {
        [Parameter] public RenderFragment ListTitle { get; set; }
        [Parameter] public IEnumerable<TItem> Items { get; set; }
        [Parameter] public RenderFragment<TItem> ItemTemplate { get; set; }
        [Parameter] public string ListClass { get; set; } = "list-group";
        [Parameter] public string ItemClass { get; set; } = "list-group-item";
        [Parameter] public bool ShowEmptyListMessage { get; set; } = true;
        [Parameter] public string EmptyListMessage { get; set; }

        private string emptyListClass => ShowEmptyListMessage ? "alert alert-warning" : "d-none";
    }
}