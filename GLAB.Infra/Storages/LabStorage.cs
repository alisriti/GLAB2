using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLab.Domains.Models.Laboratoires;
using Microsoft.Extensions.Configuration;

namespace GLAB.Infra.Storages
{
    public class LabStorage : ILabStorage
    {
        private string connectionString;

        public LabStorage(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("GLabDB");
        }

        public async Task<List<Laboratoire>> SelectLaboratoires()
        {
            List<Laboratoire> labos = new List<Laboratoire>();
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new("select * from vwLabos", connection);

            DataTable ds = new();
            SqlDataAdapter da = new(cmd);

            connection.Open();
            da.Fill(ds);

            foreach (DataRow row in ds.Rows)
            {
                Laboratoire labo = getLaboratoireFromDataRow(row);
                labos.Add(labo);
            }

            return labos;
        }

        public async Task<Laboratoire> SelectLaboratoireById(string id)
        {
            Laboratoire labos = new Laboratoire();
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new("select * from vwLabos where ID = @aId", connection);
            cmd.Parameters.AddWithValue("@aId", id);

            DataTable ds = new();
            SqlDataAdapter da = new(cmd);

            connection.Open();
            da.Fill(ds);

            if (ds.Rows.Count == 0)
                return null;

            return getLaboratoireFromDataRow(ds.Rows[0]);
        }

        public async Task<bool> LaboratoireExists(string id)
        {
            return await SelectLaboratoireById(id) != null;
        }

        public async Task InsertLaboratoire(Laboratoire laboratoire)
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new("Insert dbo.LABORATOIRES(Id, Universite, Acronyme, Nom, Address, Email) " +
                                 "VALUES(@aId, @aUniversite, @aAcronyme, @aNom, @aAddress, @aEmail)", connection);

            cmd.Parameters.AddWithValue("@aId", laboratoire.Id);
            cmd.Parameters.AddWithValue("@aUniversite", laboratoire.Universite);
            cmd.Parameters.AddWithValue("@aAcronyme", laboratoire.Acronyme);
            cmd.Parameters.AddWithValue("@aNom", laboratoire.Nom);
            cmd.Parameters.AddWithValue("@aAddress", laboratoire.Adresse);
            cmd.Parameters.AddWithValue("@aEmail", laboratoire.Email);

            connection.Open();
            await cmd.ExecuteNonQueryAsync();
        }

        private static Laboratoire getLaboratoireFromDataRow(DataRow row)
        {
            return new()
            {
                Id = (string)row["Id"],
                Acronyme = (string)row["Acronyme"],
                Adresse = (string)row["Adresse"],
                Universite = (string)row["Universite"],
                Nom = (string)row["Nom"],
                Email = (string)row["Email"],
                WebSite = (string)row["WebSite"],
            };
        }
    }
}