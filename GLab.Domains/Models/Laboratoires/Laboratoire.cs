using System.ComponentModel.Design;

namespace GLab.Domains.Models.Laboratoires
{
    public class Laboratoire
    {
        public string Id { get; set; }
        public string Universite { get; set; } = "932033522";
        public string Acronyme { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Faculte { get; set; }
        public string Departement { get; set; }
        public DateTime DateCreation { get; set; }
        public string NumAgrement { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public byte[] Logo { get; set; } = new byte[] { };
        public LaboratoireStatus Status { get; set; }
    }

    public enum LaboratoireStatus
    {
        Active = 1,
        Bloqued = 0,
        Deleted = -1
    }
}