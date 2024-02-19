using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLab.Apps.Laboratoires;
using GLab.Apps.Shared;
using GLab.Domains.Models.Laboratoires;
using GLab.Domains.Models.Shared;
using GLAB.UI.Laboratoires.Models;
using Microsoft.AspNetCore.Components;

namespace GLAB.UI.Laboratoires
{
    public partial class NewLabComponent
    {
        [Inject] public IUnivService univService { get; set; }
        [Inject] public ILabService labService { get; set; }

        private CreateLabModel newLab;

        private Universite myUniv;

        protected override void OnInitialized()
        {
            myUniv = univService.GetMyUniv();
            newLab = new CreateLabModel()
            {
                Adresse = myUniv.Adresse
            };
        }

        private async Task createLab()
        {
            Laboratoire laboratoireToCreate = new Laboratoire()
            {
                Id = Guid.NewGuid().ToString(),
                Acronyme = newLab.Acronyme,
                Nom = newLab.Nom,
                Adresse = newLab.Adresse,
                //Adresse = string.IsNullOrWhiteSpace(newLab.Adresse)
                //    ? myUniv.Adresse
                //    : newLab.Adresse,
                Email = newLab.Email,
                Universite = myUniv.PIC,
                DateCreation = DateTime.Now
            };

            await labService.CreateLaboratoire(laboratoireToCreate);
        }
    }
}