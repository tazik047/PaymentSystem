using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Services;
using DAO.Repository;
using Microsoft.AspNet.Identity;

namespace PaymentSystem.Controllers
{
    public class CardController : Controller
    {
        private readonly IRepositoryFactory _factory;

        public CardController(IRepositoryFactory factory)
        {
            _factory = factory;
        }

        // GET: Card
        public ActionResult Index()
        {
            var accounts = AccountService.GetAllAccounts(_factory, User.Identity.GetUserId());
            return View(accounts);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Details(long id = 0)
        {
            var account = AccountService.GetAccount(_factory, id, User.Identity.GetUserId(), User.IsInRole("User"));
            if(account==null)
                return new HttpNotFoundResult();
            return View(account);
        }

        public ActionResult Block(long? cardId)
        {
            return View();
        }

        public ActionResult RequertToUnBlock(long? cardId)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UnBlock(long? cardId)
        {
            return View();
        }
    }
}