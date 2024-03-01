using System.ComponentModel.DataAnnotations;

namespace GLAB.UI.Laboratoires.Models
{
    internal class CreateLabModel
    {
        [Required(ErrorMessage = "Vous devez donner le nom du laboratoire.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Vous devez donner l'acronyme du laboratoire.")]
        public string Acronyme { get; set; }

        public string Adresse { get; set; }

        [EmailAddress(ErrorMessage = "Adresse mail non valide.")]
        public string Email { get; set; }
    }
}