using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace backOfficeMvc.Models
{
    public class AdminDao
    {
        public Admin Login(string login, string password)
        {
            Admin admin = null;

            string connStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            SqlConnection sqlConnection = new SqlConnection(connStr);

            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM utilisateurs WHERE login = @login AND mdp = @password", sqlConnection);

            sqlCommand.Parameters.Add("login", SqlDbType.NVarChar).Value = login;
            sqlCommand.Parameters.Add("password", SqlDbType.NVarChar).Value = password;

            sqlConnection.Open();

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                if (sqlDataReader.GetString(3) == "administrateur")
                {

                    admin = new Admin(
                        sqlDataReader.GetInt32(0),
                        sqlDataReader.GetString(1)
                    );
                } else
                {
                    throw new Exception("L'utilisateur n'est pas un administrateur.");
                }
            }

            sqlConnection.Close();

            return admin;
        }
    }
}