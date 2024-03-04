using GLab.Domains.Models.Laboratoires;

namespace GLAB.Infra.Storages;

public interface ILabStorage
{
    Task<List<Laboratoire>> SelectLaboratoires();

    Task<Laboratoire> SelectLaboratoireById(string id);

    Task<bool> LaboratoireExists(string acronyme);

    Task InsertLaboratoire(Laboratoire laboratoire);

    Task<bool> UpdateLaboratoire(Laboratoire laboratoire);
}