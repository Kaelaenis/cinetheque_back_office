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
    public class ArticleController : Controller
    {
        private readonly IArticleDao _articleDao;

        public ArticleController(IArticleDao articleDao)
        {
            _articleDao = articleDao;
        }

        [HttpGet]
        // GET: Article
        public ActionResult List()
        {
            var isAdmin = TestAdminSession();

            if (!isAdmin)
            {
                return RedirectToAction("Login", "Admin");
            }

            List<Article> articles = _articleDao.GetAllArticles();

            var vm = new ArticleListViewModel
            {
                Articles = articles.Select(a => new ArticleViewModel(a)).ToList()
            };

            return View(vm);
        }

        [HttpGet]
        [Route("Article/{id:int}/Edit")]
        public ActionResult Edit(int id)
        {
            var isAdmin = TestAdminSession();

            if (!isAdmin)
            {
                return RedirectToAction("Login", "Admin");
            }

            Article article = _articleDao.GetArticleById(id);

            ViewBag.Categories = _articleDao.SelectAllCategories();

            var vm = new ArticleViewModel(article);

            return View(vm);
        }

        [HttpPost]
        [Route("Article/{id:int}/Edit")]
        // POST : Article/:id/Edit
        public ActionResult Edit(int id, ArticleViewModel vm)
        {

            ViewBag.Categories = _articleDao.SelectAllCategories();

            Article existingArticle = _articleDao.GetArticleById(id);

            if (existingArticle == null)
            {
                return HttpNotFound();
            }

            existingArticle.Nom = vm.Nom;
            existingArticle.Prix = vm.Prix;
            existingArticle.Categorie = vm.CategorieNom;
            existingArticle.Description = vm.Description;
            existingArticle.QteTotal = vm.QteTotal;
            existingArticle.QteDispo = vm.QteDispo;

            _articleDao.UpdateArticle(id, existingArticle);

            return RedirectToAction("List");
        }

        [HttpGet]
        [Route("Article/Create")]
        public ActionResult Create()
        {
            var isAdmin = TestAdminSession();

            if (!isAdmin)
            {
                return RedirectToAction("Login", "Admin");
            }

            ViewBag.Categories = _articleDao.SelectAllCategories();
            return View();
        }

        [HttpPost]
        [Route("Article/Create")]
        public ActionResult Create(ArticleViewModel vm) {
            ViewBag.Categories = _articleDao.SelectAllCategories();
            if (!ModelState.IsValid)
            {             
                return View(vm);
            }
            var newArticle = new Article
            {
                Nom = vm.Nom,
                Prix = vm.Prix,
                Categorie = vm.CategorieNom,
                Description = vm.Description,
                QteTotal = vm.QteTotal,
                QteDispo = vm.QteDispo
            };
            _articleDao.AddArticle(newArticle);
            return RedirectToAction("List");
        }

        [Route("Article/{id:int}/Delete")]
        public ActionResult Delete(int id)
        {
            var isAdmin = TestAdminSession();
            if (!isAdmin)
            {
                return RedirectToAction("Login", "Admin");
            }

            Article article = _articleDao.GetArticleById(id);
            if (article == null)
            {
                return HttpNotFound();
            }

            _articleDao.DeleteArticle(id);

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