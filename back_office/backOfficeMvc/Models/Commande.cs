using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backOfficeMvc.Models
{
    // Naming it "Commande" to be able to use it later for rentals and purchases
    public class Commande
    {
        public int Id { get; set; }
        public double Prix_total { get; set; }
        public int Qte_articles { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int Article_id { get; set; }
        public int Client_id { get; set; }

        public Commande(int id, double prix_total, int qte_articles, DateTime dateDebut, DateTime dateFin, int article_id, int client_id)
        {
            Id = id;
            Prix_total = prix_total;
            Qte_articles = qte_articles;
            DateDebut = dateDebut;
            DateFin = dateFin;
            Article_id = article_id;
            Client_id = client_id;
        }

        public Commande() { }
    }
}