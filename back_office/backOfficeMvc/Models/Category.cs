using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backOfficeMvc.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public Category(int id, string nom) {
            Id = id;
            Nom = nom;
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}