using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Services;
using DAO.Model;
using DAO.Repository;
using Microsoft.AspNet.Identity;

namespace PaymentSystem.Controllers
{
    public class OperationController : Controller
    {
        private readonly IRepositoryFactory _factory;

        public OperationController(IRepositoryFactory factory)
        {
            _factory = factory;
        }
        // GET: Operation
        public ActionResult Index()
        {
            ViewBag.Title = "Все операции";
            ViewBag.ActionName = "AllOperations";
            ViewBag.ShowType = true;
            return View("_OperationTable");
        }

        public ActionResult CreatePayment()
        {
            return View();
        }

        public ActionResult CreateBankOperation()
        {
            ViewBag.Accounts = AccountService.GetAccounts(_factory, User.Identity.GetUserId())
                .Select(t => new SelectListItem {Value = t.Item1, Text = t.Item2});
            return View();
        }

        public ActionResult CreateCardOperation()
        {
            ViewBag.Accounts = AccountService.GetAccounts(_factory, User.Identity.GetUserId())
                .Select(t => new SelectListItem { Value = t.Item1, Text = t.Item2 });
            return View();
        }

        public ActionResult CreateMobileOperation()
        {
            ViewBag.Accounts = AccountService.GetAccounts(_factory, User.Identity.GetUserId())
                .Select(t => new SelectListItem { Value = t.Item1, Text = t.Item2 });
            return View();
        }

        public ActionResult CreateReplenishment()
        {
            ViewBag.Accounts = AccountService.GetAccounts(_factory, User.Identity.GetUserId())
                .Select(t => new SelectListItem { Value = t.Item1, Text = t.Item2 });
            return View();
        }

        [HttpPost]
        public ActionResult CreateReplenishment(MobileOperation operation, bool? Prepared, string Account, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                
            }
            return View();
        }

        public ActionResult CreatePreparedPayment()
        {
            ViewBag.Title = "Плетежи, ожидающие подтверждения";
            ViewBag.ActionName = "PreparedOperations";
            ViewBag.ShowType = false;
            return View("_OperationTable");
        }

        public ActionResult PreparedPayments()
        {
            ViewBag.ActionName = "PreparedOperations";
            ViewBag.ShowType = false;
            return View("_OperationTable");
        }

        public ActionResult Payments()
        {
            ViewBag.Title = "Все платежи";
            ViewBag.ShowType = false;
            ViewBag.ActionName = "PaymnetOperations";
            return View("_OperationTable");
        }

        public ActionResult Replenishments()
        {
            ViewBag.Title = "Все пополнения";
            ViewBag.ActionName = "ReplenishmentOperations";
            ViewBag.ShowType = false;
            return View("_OperationTable");
        }

        public ActionResult Details(long id = 0)
        {
            var operation = OperationServices.GetOperation(_factory, id);
            if(operation==null)
                return new HttpNotFoundResult();
            return View(operation.GetType().Name.Split('_')[0], operation);
        }
    }
}