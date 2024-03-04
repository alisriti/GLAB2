using GLab.Domains.Models.Shared;

namespace GLab.Domains.Models.Laboratoires;

public static class LaboratoireErrors
{
    public static ErrorCode StatusBloqued =
        new ErrorCode("LaboratoireError.StatusBloqued", "Le laboratoire n'existe pas");

    public static ErrorCode AcronymeEmpty =
        new ErrorCode("LaboratoireError.AcronymeEmpty", "l'acronyme ne peut pas etre vide");

    public static ErrorCode NomEmpty =
        new ErrorCode("LaboratoireError.NomEmpty", $"Le nom ne peut pas etre vide");

    public static ErrorCode AdresseEmpty =
        new ErrorCode("LaboratoireError.AdresseEmpty", $"l'adresse ne peut pas etre vide");
}