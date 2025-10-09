using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backOfficeMvc.Models.ViewModels
{
    public class CommandeViewModel
    {
        public int Id { get; set; }
        public double Prix_total { get; set; }
        public int Qte_articles { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int Article_id { get; set; }
        public int Client_id { get; set; }

        public CommandeViewModel (Commande commande)
        {
            Id = commande.Id;
            Prix_total = commande.Prix_total;
            Qte_articles = commande.Qte_articles;
            DateDebut = commande.DateDebut;
            DateFin = commande.DateFin;
            Article_id = commande.Article_id;
            Client_id = commande.Client_id;
        }

        public CommandeViewModel() { }
    }
}