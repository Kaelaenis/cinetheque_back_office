using backOfficeMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backOfficeMvc.DataAccess
{
    public interface IAdminDao
    {
        Admin Login(string login, string password);
    }
}
