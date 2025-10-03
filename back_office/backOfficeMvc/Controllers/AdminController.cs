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

        //public ActionResult Login(string username, string password)
        //{
        //    return View();
        //}

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}