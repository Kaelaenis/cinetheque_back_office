using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backOfficeMvc.Models.ViewModels
{
    public class CommandeListViewModel
    {
        public List<CommandeViewModel> Commandes { get; set; } = new List<CommandeViewModel>();
    }
}