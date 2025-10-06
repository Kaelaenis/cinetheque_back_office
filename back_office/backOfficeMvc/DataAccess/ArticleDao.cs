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
                    sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("id")),
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

        public List<Category> SelectAllCategories()
        {
            List<Category> categories = new List<Category>();
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM categories", sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Category category = new Category(
                    sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("id")),
                    sqlDataReader.GetString(sqlDataReader.GetOrdinal("categorie"))
                );
                categories.Add(category);
            }
            sqlConnection.Close();
            return categories;
        }

        public List<Article> GetArticlesByCategory(string category)
        {
            List<Article> articles = new List<Article>();
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT a.*, c.categorie AS category_name FROM articles a INNER JOIN categories c on a.categorie_id = c.id WHERE c.categorie = @category", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@category", category);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Article article = new Article(
                    sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("id")),
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

        public void AddArticle(Article article)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO articles (nom, prix, categorie_id, description, qte_totale, qte_dispo) VALUES (@nom, @prix, (SELECT id FROM categories WHERE categorie = @categorie), @description, @qte_totale, @qte_dispo)", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@nom", article.Nom);
            sqlCommand.Parameters.AddWithValue("@prix", article.Prix);
            sqlCommand.Parameters.AddWithValue("@categorie", article.Categorie);
            sqlCommand.Parameters.AddWithValue("@description", article.Description);
            sqlCommand.Parameters.AddWithValue("@qte_totale", article.QteTotal);
            sqlCommand.Parameters.AddWithValue("@qte_dispo", article.QteDispo);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void DeleteArticle(int articleId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand("DELETE FROM articles WHERE id = @id", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", articleId);

            sqlConnection.Open();

            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void UpdateArticle(int articleId, Article updatedArticle)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand("UPDATE articles SET nom = @nom, prix = @prix, categorie_id = (SELECT id FROM categories WHERE categorie = @categorie), description = @description, qte_totale = @qte_totale, qte_dispo = @qte_dispo WHERE id = @id", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", articleId);
            sqlCommand.Parameters.AddWithValue("@nom", updatedArticle.Nom);
            sqlCommand.Parameters.AddWithValue("@prix", updatedArticle.Prix);
            sqlCommand.Parameters.AddWithValue("@categorie", updatedArticle.Categorie);
            sqlCommand.Parameters.AddWithValue("@description", updatedArticle.Description);
            sqlCommand.Parameters.AddWithValue("@qte_totale", updatedArticle.QteTotal);
            sqlCommand.Parameters.AddWithValue("@qte_dispo", updatedArticle.QteDispo);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public Article GetArticleById(int articleId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT a.*, c.categorie AS category_name FROM articles a INNER JOIN categories c on a.categorie_id = c.id WHERE a.id = @id", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", articleId);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            Article article = null;

            if (sqlDataReader != null)
            {
                while (sqlDataReader.Read())
                {
                    int prixIndex = sqlDataReader.GetOrdinal("prix");

                    double prix = 0.0;
                    if (!sqlDataReader.IsDBNull(prixIndex))
                    {
                        prix = Convert.ToDouble(sqlDataReader.GetDouble(prixIndex));
                    }

                    article = new Article(
                        sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("id")),
                        sqlDataReader.GetString(sqlDataReader.GetOrdinal("nom")),
                        prix,
                        sqlDataReader.GetString(sqlDataReader.GetOrdinal("category_name")),
                        sqlDataReader.GetString(sqlDataReader.GetOrdinal("description")),
                        sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("qte_totale")),
                        sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("qte_dispo"))
                    );
                }
            }

            sqlConnection.Close();
            return article;
        }
    }
}