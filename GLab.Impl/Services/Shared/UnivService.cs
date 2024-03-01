using GLab.Apps.Shared;
using GLab.Domains.Models.Shared;

namespace GLab.Impl.Services.Shared
{
    public class UnivService : IUnivService
    {
        public Universite GetMyUniv()
        {
            return new()
            {
                Code = "BISKRA",
                PIC = "932033522",
                Nom = "Universite Med Khider, Biskra",
                Adresse = "Route de Sidi Okba, 07000, Biskra."
            };
        }
    }
}