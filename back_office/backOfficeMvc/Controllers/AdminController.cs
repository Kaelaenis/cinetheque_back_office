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
                return RedirectToAction("Index", "Home");
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

            if (adminId == null || adminLogin == null)
            {
                return RedirectToAction("Login");
            }

            var articles = _articleDao.GetAllArticles();

            var chartData = articles
               .GroupBy(a => a.Categorie)
               .Select(g => new ChartCategoryViewModel
               {
                   Category = g.Key,
                   Count = g.Count()
               })
               .ToList();

            // (Optionnel) pour peupler ton <select>
            ViewBag.Categories = _articleDao.SelectAllCategories();

            return View(chartData); // => ton modèle principal
        }


        [HttpPost]
        public ActionResult Dashboard(string categoryFilter)
        {
            var adminId = Session["AdminId"];
            var adminLogin = Session["AdminLogin"];

            if (adminId == null || adminLogin == null)
            {
                return Json(new { success = false, message = "Session expirée" });
            }

            // Récupère les articles de la catégorie choisie
            var articles = _articleDao.GetArticlesByCategory(categoryFilter);

            // Construit la liste des articles pour le graphique filtré
            var chartData = articles
               .Select(a => new ChartArticleViewModel
               {
                   Name = a.Nom,
                   QteTotale = a.QteTotal
               })
               .ToList();

            return Json(new { success = true, data = chartData });
        }


        // Admin/logout
        public ActionResult Logout()
        {
            return View();
        }
    }
}