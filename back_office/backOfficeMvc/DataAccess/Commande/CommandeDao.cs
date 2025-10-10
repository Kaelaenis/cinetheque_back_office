using backOfficeMvc.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace backOfficeMvc.DataAccess
{
    public class CommandeDao: ICommandeDao
    {
        public void AddCommande(Commande commande)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connStr);

            SqlCommand sqlCommand = new SqlCommand("INSERT INTO locations (prix_total, qte_articles, date_debut, date_fin, article_id, utilisateur_id) VALUES (@prix_total, @qte_articles, @date_debut, @date_fin, @article_id, @utilisateur_id)", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@prix_total", commande.Prix_total);
            sqlCommand.Parameters.AddWithValue("@qte_articles", commande.Qte_articles);
            sqlCommand.Parameters.AddWithValue("@date_debut", commande.DateDebut);
            sqlCommand.Parameters.AddWithValue("@date_fin", commande.DateFin);
            sqlCommand.Parameters.AddWithValue("@article_id", commande.Article_id);
            sqlCommand.Parameters.AddWithValue("@utilisateur_id", commande.Client_id);

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public void UpdateCommande(int commandeId, Commande commande)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand("UPDATE locations SET prix_total = @prix_total, qte_articles = @qte_articles, date_debut = @date_debut, date_fin = @date_fin, article_id = @article_id, utilisateur_id = @utilisateur_id WHERE id = @id", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@prix_total", commande.Prix_total);
            sqlCommand.Parameters.AddWithValue("@qte_articles", commande.Qte_articles);
            sqlCommand.Parameters.AddWithValue("@date_debut", commande.DateDebut);
            sqlCommand.Parameters.AddWithValue("@date_fin", commande.DateFin);
            sqlCommand.Parameters.AddWithValue("@article_id", commande.Article_id);
            sqlCommand.Parameters.AddWithValue("@utilisateur_id", commande.Client_id);
            sqlCommand.Parameters.AddWithValue("@id", commandeId);

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public void RemoveCommande(int commandeId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand("DELETE FROM locations WHERE id = @id", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", commandeId);

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public Commande GetCommande(int commandeId)
        {
            Commande commande = null;

            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM locations WHERE id = @id", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", commandeId);

            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.Read())
            {
                commande = new Commande(
                    sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("id")),
                    sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("prix_total")),
                    sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("qte_articles")),
                    sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("date_debut")),
                    sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("date_fin")),
                    sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("article_id")),
                    sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("utilisateur_id"))
                );
            }
            sqlConnection.Close();
            return commande;
        }
        public List<Commande> GetAllCommandes()
        {
            List<Commande> commandes = new List<Commande>();

            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connStr);

            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM locations", sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Commande commande = new Commande(
                    sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("id")),
                    sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("prix_total")),
                    sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("qte_articles")),
                    sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("date_debut")),
                    sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("date_fin")),
                    sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("article_id")),
                    sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("utilisateur_id"))
                );
                commandes.Add(commande);
            }

            sqlConnection.Close();

            return commandes;

        }
    }
}