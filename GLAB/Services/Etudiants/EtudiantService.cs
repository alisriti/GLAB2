using GLAB.Components.Pages;
using System.Data.SqlClient;
using System.Data;

namespace GLAB.Services.Etudiants
{
    public class EtudiantService
    {
        public async Task<List<Etudiant>> GetEtudiants()
        {
            List<Etudiant> etudiants = new List<Etudiant>();

            await using var connection = new SqlConnection("Server=laptop\\mssql;Database=GLAB;Trusted_Connection=True;MultipleActiveResultSets=true");
            SqlCommand cmd = new("select * from ETUDIANTS", connection);

            DataTable ds = new();
            SqlDataAdapter da = new(cmd);

            connection.Open();
            da.Fill(ds);

            foreach (DataRow row in ds.Rows)
            {
                Etudiant etudiant = new Etudiant()
                {
                    Id = (int)row["Id"],
                    Nom = (string)row["Nom"],
                    Prenom = (string)row["Prenom"]
                };
                etudiants.Add(etudiant);
            }

            return etudiants;
        }
    }
}