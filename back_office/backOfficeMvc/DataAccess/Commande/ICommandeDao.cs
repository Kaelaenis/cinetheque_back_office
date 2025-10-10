using backOfficeMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backOfficeMvc.DataAccess
{
    public interface ICommandeDao
    {
        void AddCommande(Commande commande);
        void UpdateCommande(int commandeId, Commande commande);
        void RemoveCommande(int commandeId);
        Commande GetCommande(int commandeId);
        List<Commande> GetAllCommandes();
    }
}
