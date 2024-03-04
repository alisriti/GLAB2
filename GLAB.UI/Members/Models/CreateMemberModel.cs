using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLAB.UI.Members.Models
{
    public class CreateMemberModel
    {
        [Required(ErrorMessage = "Vous devez donner le nom du membre.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Vous devez donner le prénom du membre.")]
        public string Prenom { get; set; }

        [EmailAddress(ErrorMessage = "Adresse mail non valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vous devez sélectionner l'équipe membre.")]
        public string Equipe { get; set; }
    }
}