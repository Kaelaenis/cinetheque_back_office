using backOfficeMvc.DataAccess;
using backOfficeMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace backOfficeMvc.Controllers
{
    public class AdminController : Controller
    {
        private readonly IArticleDao _articleDao;
        private readonly IAdminDao _adminDao;
        public AdminController(IAdminDao adminDao, IArticleDao articleDao)
        {
            _adminDao = adminDao;
            _articleDao = articleDao;
        }

        // Admin/login
        public ActionResult Login()
        {
            return View();
        }

        // Admin/login
        [HttpPost]
        public ActionResult Login(string login, string password)
        {
            Admin admin = _adminDao.Login(login, password);

            if (admin != null)
            {
                Session["AdminId"] = admin.Id;
                Session["AdminLogin"] = admin.Login;
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }
        }

        // Admin/dashboard
        public ActionResult Dashboard()
        {
            var adminId = Session["AdminId"];
            var adminLogin = Session["AdminLogin"];

            if (adminId == null || adminLogin == null)
            {
                return RedirectToAction("Login");
            }   

            return View();
        }

        // Admin/logout
        public ActionResult Logout()
        {
            return View();
        }
    }
}