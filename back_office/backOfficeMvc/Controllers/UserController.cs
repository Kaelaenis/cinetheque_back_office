using backOfficeMvc.DataAccess;
using backOfficeMvc.Models;
using backOfficeMvc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backOfficeMvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserDao _userDao;

        public UserController(IUserDao userDao)
        {
            _userDao = userDao;
        }
        // GET: User/List
        [Route("User/List")]
        public ActionResult List()
        {
            var isAdmin = TestAdminSession();

            if (!isAdmin)
            {
                return RedirectToAction("Login", "Admin");
            }

            var users = _userDao.GetAllUsers();

            var vm = new UserListViewModel
            {
                Users = users.Select(u => new UserViewModel(u)).ToList()
            };

            return View(vm);
        }

        [HttpGet]
        [Route("User/Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("User/Create")]
        public ActionResult Create(UserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = new User
            {
                Id = vm.Id,
                Nom = vm.Nom,
                Prenom = vm.Prenom,
                Adresse = vm.Adresse,
                Login = vm.Login,
                Mdp = vm.Mdp,
                Role = vm.Role
            };

            _userDao.AddUser(user);
            return RedirectToAction("List");
        }

        [HttpGet]
        [Route("User/{id:int}/Edit")]
        public ActionResult Edit(int id)
        {
            User user = _userDao.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var vm = new UserViewModel(user);

            return View(vm);
        }

        [HttpPost]
        [Route("User/{id:int}/Edit")]
        public ActionResult Edit(int id, UserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var user = new User
            {
                Id = vm.Id,
                Nom = vm.Nom,
                Prenom = vm.Prenom,
                Adresse = vm.Adresse,
                Login = vm.Login,
                Mdp = vm.Mdp,
                Role = vm.Role
            };
            _userDao.UpdateUser(id, user);
            return RedirectToAction("List");
        }

        [Route("User/{id:int}/Delete")]
        public ActionResult Delete(int id)
        {
            User user = _userDao.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            _userDao.RemoveUser(id);
            return RedirectToAction("List");
        }

        private bool TestAdminSession()
        {
            var adminId = Session["AdminId"];
            var adminLogin = Session["AdminLogin"];
            return adminId != null && adminLogin != null;
        }
    }
}