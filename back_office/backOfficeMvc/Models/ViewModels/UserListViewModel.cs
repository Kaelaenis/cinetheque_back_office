using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backOfficeMvc.Models.ViewModels
{
    public class UserListViewModel
    {
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }
}