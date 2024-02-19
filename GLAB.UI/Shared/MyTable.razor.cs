using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace GLAB.UI.Shared
{
    public partial class MyTable<T>
    {
        [Parameter] public List<T> Items { get; set; }
        [Parameter] public RenderFragment TableHeader { get; set; }
        [Parameter] public RenderFragment<T> RowTemplate { get; set; }
    }
}