using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
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
            //var accounts = AccountService.GetAllAccounts(_factory, User.Identity.GetUserId());
            return View();
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

        public ActionResult BlockedAccounts()
        {
            return View();
        }

        public ActionResult Block(long id = 0)
        {
            try
            {
                AccountService.BlockAccount(_factory, id, User.Identity.GetUserId(), User.IsInRole("User"));
                return Content("Счет успешно заблокирован");
            }
            catch (ValidationException e)
            {
                return new HttpNotFoundResult(e.Message);
            }
            
        }

        public ActionResult RequertToUnBlock(long id = 0)
        {
            return Content("Запрос отправлен администратору на разблокировку.");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UnBlock(long id = 0)
        {
            AccountService.UnBlockAccount(_factory, id);
            return Content("Аккаунт разблокирован.");
        }
    }
}