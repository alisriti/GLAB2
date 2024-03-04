using GLab.Domains.Models.Laboratoires;
using GLab.Domains.Models.Shared;

namespace GLab.Apps.Laboratoires;

public interface ILabService
{
    Task<List<Laboratoire>> GetLaboratoires();

    Task<Laboratoire> GetLaboratoireById(string id);

    Task<bool> LaboratoireExists(string id);

    Task CreateLaboratoire(Laboratoire laboratoire);

    Task<Result> UpdateLaboratoire(Laboratoire laboratoire);
}