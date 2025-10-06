using backOfficeMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backOfficeMvc.DataAccess
{
    public interface IUserDao
    {
        // Define method signatures for user data access operations
        void AddUser(User user);
        void UpdateUser(int userId, User user);
        void RemoveUser(int userId);
        User GetUser(int userId);
        List<User> GetAllUsers();
    }
}
