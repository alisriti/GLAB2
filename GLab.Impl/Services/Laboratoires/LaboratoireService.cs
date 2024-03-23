using GLab.Apps.Laboratoires;
using GLab.Domains;
using GLab.Domains.Models.Laboratoires;
using GLab.Domains.Models.Shared;
using GLAB.Infra.Storages;

namespace GLab.Impl.Services.Laboratoires
{
    public class LaboratoireService : ILabService
    {
        private readonly ILabStorage labStorage;

        public LaboratoireService(ILabStorage labStorage)
        {
            this.labStorage = labStorage;
        }

        public async Task<Result<Laboratoire>> GetLaboratoireById(string id)
        {
            var laboratoire =  await labStorage.SelectLaboratoireById(id);
            return Result<Laboratoire>.Succes(laboratoire);
        }

        public async Task<bool> LaboratoireExists(string acronyme)
        {
            return await labStorage.LaboratoireExists(acronyme);
        }

        public async Task CreateLaboratoire(Laboratoire laboratoire)
        {
            validateLaboratoireForInsert(laboratoire);

            if (await LaboratoireExists(laboratoire.Acronyme))
                throw new Exception($"Acronyme {laboratoire.Acronyme} existe déja");
            laboratoire.Id = "1C12F274-D5F9-40AE-B4A3-69F47AC835B8";
            await labStorage.InsertLaboratoire(laboratoire);
        }

        public async Task<Result> UpdateLaboratoire(Laboratoire laboratoire)
        {
            List<ErrorCode> errorList = validateLaboratoireForUpdate(laboratoire);
            if (errorList.Any())
                return Result.Failure(errorList.Select(e => e.Message).ToList());

            await labStorage.UpdateLaboratoire(laboratoire);
            return Result.Succes;
        }

        private List<ErrorCode> validateLaboratoireForUpdate(Laboratoire laboratoire)
        {
            List<ErrorCode> errors = new List<ErrorCode>();

            if (laboratoire.Status == LaboratoireStatus.Deleted)
                errors.Add(LaboratoireErrors.StatusBloqued);

            if (string.IsNullOrWhiteSpace(laboratoire.Acronyme))
                errors.Add(LaboratoireErrors.AcronymeEmpty);

            if (string.IsNullOrWhiteSpace(laboratoire.Nom))
                errors.Add(LaboratoireErrors.NomEmpty);

            if (string.IsNullOrWhiteSpace(laboratoire.Adresse))
                errors.Add(LaboratoireErrors.AdresseEmpty);

            return errors;
        }

        private void validateLaboratoireForInsert(Laboratoire laboratoire)
        {
            if (laboratoire is null ||
                string.IsNullOrWhiteSpace(laboratoire.Id) ||
                string.IsNullOrWhiteSpace(laboratoire.Nom) ||
                string.IsNullOrWhiteSpace(laboratoire.Acronyme) ||
                string.IsNullOrWhiteSpace(laboratoire.Adresse) ||
                string.IsNullOrWhiteSpace(laboratoire.Email))
                throw new Exception("Erreur de validation du laboratoire");
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