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
        // Admin/login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string login, string password)
        {
            AdminDao adminDao = new AdminDao();

            Admin admin = adminDao.Login(login, password);

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

        public ActionResult Dashboard()
        {
            var adminId = Session["AdminId"];
            var adminLogin = Session["AdminLogin"];


            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}