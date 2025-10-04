using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backOfficeMvc.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public double Prix { get; set; }
        public string Categorie { get; set; }
        public string Description { get; set; }
        public int QteTotal { get; set; }
        public int QteDispo { get; set; }

        public Article(string nom, double prix, string categorie, string description, int qteTotal, int qteDispo)
        {
            Nom = nom;
            Prix = prix;
            Categorie = categorie;
            Description = description;
            QteTotal = qteTotal;
            QteDispo = qteDispo;
        }

        public override string ToString()
        {
            return Nom + " - " + Prix + "€ - " + Categorie + " - " + Description + " - " + QteTotal + " - " + QteDispo;
        }
    }
}