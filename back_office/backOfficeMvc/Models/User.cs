using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backOfficeMvc.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Login { get; set; }
        public string Mdp { get; set; }
        public string Role { get; set; }

        public User(int id, string nom, string prenom, string adresse, string login, string mdp, string role)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            Login = login;
            Mdp = mdp;
            Role = role;
        }

        public User() { }

        public override string ToString()
        {
            return Nom + " " + Prenom + " - " + Adresse + " - " + Login + " - " + Role;
        }
    }
}