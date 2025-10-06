using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backOfficeMvc.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Login { get; set; }
        public string Mdp { get; set; }
        public string Role { get; set; }

        public UserViewModel(User user)
        {
            Id = user.Id;
            Nom = user.Nom;
            Prenom = user.Prenom;
            Adresse = user.Adresse;
            Login = user.Login;
            Mdp = user.Mdp;
            Role = user.Role;
        }

        public UserViewModel() { }
    }
}