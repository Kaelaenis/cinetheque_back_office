using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backOfficeMvc.Models.ViewModels
{
    public class ArticleViewModel
    {
        public string Nom { get; set; }
        public double Prix { get; set; }
        public string CategorieNom { get; set; }
        public string Description { get; set; }
        public int QteTotal { get; set; }
        public int QteDispo { get; set; }

        // Constructeur pour transformer un Article en ViewModel
        public ArticleViewModel(Article article)
        {
            Nom = article.Nom;
            Prix = article.Prix;
            CategorieNom = article.Categorie; // null-safe
            Description = article.Description;
            QteTotal = article.QteTotal;
            QteDispo = article.QteDispo;
        }
    }
}