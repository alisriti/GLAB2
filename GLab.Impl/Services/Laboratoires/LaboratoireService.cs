using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using GLab.Apps.Laboratoires;
using GLab.Domains.Models.Laboratoires;
using GLAB.Infra.Storages;
using Microsoft.Extensions.Configuration;

namespace GLab.Impl.Services.Laboratoires
{
    public class LaboratoireService : ILabService
    {
        private readonly ILabStorage labStorage;

        public LaboratoireService(ILabStorage labStorage)
        {
            this.labStorage = labStorage;
        }

        public async Task<Laboratoire> GetLaboratoireById(string id)
        {
            return await labStorage.SelectLaboratoireById(id);
        }

        public async Task<bool> LaboratoireExists(string id)
        {
            return await labStorage.LaboratoireExists(id);
        }

        public async Task CreateLaboratoire(Laboratoire laboratoire)
        {
            if (await laboratoireIsValidForInsert(laboratoire))
            {
                await labStorage.InsertLaboratoire(laboratoire);
            }
        }

        private async Task<bool> laboratoireIsValidForInsert(Laboratoire laboratoire)
        {
            if (laboratoire is null)
                return false;
            if (string.IsNullOrWhiteSpace(laboratoire.Id))
                return false;
            if (string.IsNullOrWhiteSpace(laboratoire.Nom))
                return false;
            if (string.IsNullOrWhiteSpace(laboratoire.Acronyme))
                return false;
            if (string.IsNullOrWhiteSpace(laboratoire.Adresse))
                return false;
            if (string.IsNullOrWhiteSpace(laboratoire.Email))
                return false;

            if (await LaboratoireExists(laboratoire.Id))
                return false;

            return true;
        }

        public async Task<List<Laboratoire>> GetLaboratoires()
        {
            return await labStorage.SelectLaboratoires();
        }
    }
}