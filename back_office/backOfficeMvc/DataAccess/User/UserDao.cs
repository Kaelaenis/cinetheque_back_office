using backOfficeMvc.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace backOfficeMvc.DataAccess
{
    public class UserDao : IUserDao
    {
        public void AddUser(User user)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connStr);
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO utilisateurs (login, mdp, role) VALUES (@login, @mdp, @role)", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@login", user.Login);
            sqlCommand.Parameters.AddWithValue("@mdp", user.Mdp);
            sqlCommand.Parameters.AddWithValue("@role", user.Role);

            SqlCommand sqlCommand2 = new SqlCommand("INSERT INTO utilisateur_infos (nom, prenom, adresse, utilisateur_id) VALUES (@nom, @prenom, @adresse, (SELECT TOP 1 id FROM utilisateurs WHERE login = @login ORDER BY id DESC))", sqlConnection);
            sqlCommand2.Parameters.AddWithValue("@nom", user.Nom);
            sqlCommand2.Parameters.AddWithValue("@prenom", user.Prenom);
            sqlCommand2.Parameters.AddWithValue("@adresse", user.Adresse);
            sqlCommand2.Parameters.AddWithValue("@login", user.Login);

            sqlConnection.Open();

            sqlCommand.ExecuteNonQuery();
            sqlCommand2.ExecuteNonQuery();
            sqlConnection.Close();

        }
        public void UpdateUser(int userId, User user)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            SqlConnection sqlConnection = new SqlConnection(connStr);

            SqlCommand sqlCommand = new SqlCommand("UPDATE utilisateurs SET login = @login, mdp = @mdp, role = @role WHERE id = @userId", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@login", user.Login);
            sqlCommand.Parameters.AddWithValue("@mdp", user.Mdp);
            sqlCommand.Parameters.AddWithValue("@role", user.Role);
            sqlCommand.Parameters.AddWithValue("@userId", userId);

            SqlCommand sqlCommand2 = new SqlCommand("UPDATE utilisateur_infos SET nom = @nom, prenom = @prenom, adresse = @adresse WHERE utilisateur_id = @userId", sqlConnection);
            sqlCommand2.Parameters.AddWithValue("@nom", user.Nom);
            sqlCommand2.Parameters.AddWithValue("@prenom", user.Prenom);
            sqlCommand2.Parameters.AddWithValue("@adresse", user.Adresse);
            sqlCommand2.Parameters.AddWithValue("@userId", userId);

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlCommand2.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public void RemoveUser(int userId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            SqlConnection sqlConnection = new SqlConnection(connStr);

            SqlCommand sqlCommand = new SqlCommand("DELETE FROM utilisateur_infos WHERE utilisateur_id = @userId", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@userId", userId);
            SqlCommand sqlCommand2 = new SqlCommand("DELETE FROM utilisateurs WHERE id = @userId", sqlConnection);
            sqlCommand2.Parameters.AddWithValue("@userId", userId);

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlCommand2.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public User GetUser(int userId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            User user = null;

            SqlConnection sqlConnection = new SqlConnection(connStr);

            SqlCommand sqlCommand = new SqlCommand("SELECT u.id, ui.nom, ui.prenom, ui.adresse, u.login, u.mdp, u.role FROM utilisateurs u INNER JOIN utilisateur_infos ui ON u.id = ui.utilisateur_id WHERE u.id = @userId", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@userId", userId);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            
            if (sqlDataReader.Read())
            {
                user = new User(
                    sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("id")),
                    sqlDataReader.GetString(sqlDataReader.GetOrdinal("nom")),
                    sqlDataReader.GetString(sqlDataReader.GetOrdinal("prenom")),
                    sqlDataReader.GetString(sqlDataReader.GetOrdinal("adresse")),
                    sqlDataReader.GetString(sqlDataReader.GetOrdinal("login")),
                    sqlDataReader.GetString(sqlDataReader.GetOrdinal("mdp")),
                    sqlDataReader.GetString(sqlDataReader.GetOrdinal("role"))
                );
            }
            sqlConnection.Close();
            return user;
        }
        public List<User> GetAllUsers()
        {
            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            List<User> users = new List<User>();

            SqlConnection sqlConnection = new SqlConnection(connStr);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT u.id, ui.nom, ui.prenom, ui.adresse, u.login, u.mdp, u.role FROM utilisateurs u INNER JOIN utilisateur_infos ui ON u.id = ui.utilisateur_id", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                User user = new User(
                    sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("id")),
                    sqlDataReader.GetString(sqlDataReader.GetOrdinal("nom")),
                    sqlDataReader.GetString(sqlDataReader.GetOrdinal("prenom")),
                    sqlDataReader.GetString(sqlDataReader.GetOrdinal("adresse")),
                    sqlDataReader.GetString(sqlDataReader.GetOrdinal("login")),
                    sqlDataReader.GetString(sqlDataReader.GetOrdinal("mdp")),
                    sqlDataReader.GetString(sqlDataReader.GetOrdinal("role"))
                );
                users.Add(user);
            }

            sqlConnection.Close();
            return users;
        }
    }
}