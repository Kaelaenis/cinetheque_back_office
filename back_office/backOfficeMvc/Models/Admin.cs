using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backOfficeMvc.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }

        // Default constructor
        public Admin() { }

        // Constructor for login purposes
        public Admin(int id, string username, string password)
        {
            Id = id;
            Login = username;
            Password = password;
        }

        // Full constructor for complete admin details
        public Admin(int id, string username, string password, string nom, string prenom, string adresse)
        {
            Id = id;
            Login = username;
            Password = password;
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
        }

        public override string ToString()
        {
            return $"Admin [Id={Id}, Login={Login}, Password={Password}]";
        }
    }
}