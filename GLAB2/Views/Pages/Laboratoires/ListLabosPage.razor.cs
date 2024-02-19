using GLab.Apps.Laboratoires;
using GLab.Domains.Models.Laboratoires;

namespace GLAB2.Views.Pages.Laboratoires
{
    public partial class ListLabosPage
    {
        private List<Laboratoire> labos = new List<Laboratoire>();
        private string errorMessage = String.Empty;

        private async Task getLabos()
        {
            labos = await laboService.GetLaboratoires();
        }

        private async Task gotoLab(string id)
        {
            errorMessage = String.Empty;

            if (string.IsNullOrWhiteSpace(id))
            {
                errorMessage = "aucun id fourni";
                StateHasChanged();

                return;
            }

            if (await laboService.LaboratoireExists(id) == false)
            {
                errorMessage = "Id non existant";
                StateHasChanged();
                return;
            }

            navManager.NavigateTo(url(id));
        }

        private string url(string id) => $"/labos/byid/{id}";
    }
}