using backOfficeMvc.Models;
using backOfficeMvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace backOfficeMvc.DataAccess
{
    public class ArticleDao : IArticleDao
    {
        public List<Article> GetAllArticles()
        {
            List<Article> articles = new List<Article>();

            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            SqlConnection sqlConnection = new SqlConnection(connStr);

            SqlCommand sqlCommand = new SqlCommand("SELECT a.*, c.categorie AS category_name FROM articles a INNER JOIN categories c on a.categorie_id = c.id", sqlConnection);

            sqlConnection.Open();

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Article article = new Article(
                    sqlDataReader.GetString(sqlDataReader.GetOrdinal("nom")),
                    sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("prix")),
                    sqlDataReader.GetString(sqlDataReader.GetOrdinal("category_name")),
                    sqlDataReader.GetString(sqlDataReader.GetOrdinal("description")),
                    sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("qte_totale")),
                    sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("qte_dispo"))
                );
                articles.Add(article);
            }

            sqlConnection.Close();
            return articles;
        }
    }
}