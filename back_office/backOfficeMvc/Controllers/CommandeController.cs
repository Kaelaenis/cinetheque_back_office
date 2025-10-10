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
    public class CommandeController : Controller
    {
        private readonly ICommandeDao _commandeDao;
        private readonly IArticleDao _articleDao;
        private readonly IUserDao _userDao;

        public CommandeController(ICommandeDao commandeDao, IArticleDao articleDao, IUserDao userDao)
        {
            _commandeDao = commandeDao;
            _articleDao = articleDao;
            _userDao = userDao;
        }
        // GET: Commande
        [Route("Commande/List")]
        public ActionResult List()
        {
            var isAdmin = TestAdminSession();

            if (!isAdmin)
            {
                return RedirectToAction("Login", "Admin");
            }

            List<Commande> commandes = _commandeDao.GetAllCommandes();

            var vm = new CommandeListViewModel
            {
                Commandes = commandes.Select(c => new CommandeViewModel(c)).ToList()
            };

            return View(vm);
        }

        [HttpGet]
        [Route("Commande/Create")]
        public ActionResult Create()
        {
            ViewBag.Articles = _articleDao.GetAllArticles();
            ViewBag.Clients = _userDao.GetAllUsers();

            var vm = new CommandeViewModel();
            return View(vm);
        }

        [HttpPost]
        [Route("Commande/Create")]
        public ActionResult Create(CommandeViewModel vm)
        {
            ViewBag.Articles = _articleDao.GetAllArticles();
            ViewBag.Clients = _userDao.GetAllUsers();
            if (ModelState.IsValid)
            {
                Article article = _articleDao.GetArticleById(vm.Article_id);

                Commande commande = new Commande
                {
                    Prix_total = article.Prix * vm.Qte_articles,
                    Qte_articles = vm.Qte_articles,
                    DateDebut = vm.DateDebut,
                    DateFin = vm.DateFin,
                    Article_id = vm.Article_id,
                    Client_id = vm.Client_id
                };
                _commandeDao.AddCommande(commande);
                return RedirectToAction("List");
            }
            return View(vm);
        }

        [HttpGet]
        [Route("Commande/{id}/Edit")]
        public ActionResult Edit(int id)
        {
            ViewBag.Articles = _articleDao.GetAllArticles();
            ViewBag.Clients = _userDao.GetAllUsers();
            Commande commande = _commandeDao.GetCommande(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            var vm = new CommandeViewModel(commande);
            return View(vm);
        }

        [HttpPost]
        [Route("Commande/{id}/Edit")]
        public ActionResult Edit(int id, CommandeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Commande commande = new Commande
                {
                    Id = vm.Id,
                    Prix_total = vm.Prix_total,
                    Qte_articles = vm.Qte_articles,
                    DateDebut = vm.DateDebut,
                    DateFin = vm.DateFin,
                    Article_id = vm.Article_id,
                    Client_id = vm.Client_id
                };
                _commandeDao.UpdateCommande(id, commande);
                return RedirectToAction("List");
            }
            return View(vm);
        }

        [Route("Commande/{id}/Delete")]
        public ActionResult Delete(int id)
        {
            Commande commande = _commandeDao.GetCommande(id);
            if (commande == null)
            {
                return HttpNotFound();
            }

            _commandeDao.RemoveCommande(id);
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