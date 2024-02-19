using GLab.Domains.Models.Laboratoires;

namespace GLab.Apps.Laboratoires;

public interface ILabService
{
    Task<List<Laboratoire>> GetLaboratoires();

    Task<Laboratoire> GetLaboratoireById(string id);

    Task<bool> LaboratoireExists(string id);

    Task CreateLaboratoire(Laboratoire laboratoire);
}