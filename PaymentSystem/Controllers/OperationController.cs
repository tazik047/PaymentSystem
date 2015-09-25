using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
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

        private ActionResult CreateOperation(Operation operation, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (form["Prepared"].Contains("true"))
                        PaymentService.PreparePayment(operation, _factory, User.Identity.GetUserId());
                    else
                        PaymentService.PayPayment(operation, _factory, User.Identity.GetUserId());
                    TempData["SuccessMessage"] = "Операция выполненая успешно.";
                    return RedirectToAction("Index", "Home");
                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Property, e.Message);
                }
            }
            ViewBag.Accounts = AccountService.GetAccounts(_factory, User.Identity.GetUserId())
                .Select(t => new SelectListItem { Value = t.Item1, Text = t.Item2 });
            return View(operation);
        }

        public ActionResult CreateBankOperation()
        {
            ViewBag.Accounts = AccountService.GetAccounts(_factory, User.Identity.GetUserId())
                .Select(t => new SelectListItem {Value = t.Item1, Text = t.Item2});
            return View();
        }

        [HttpPost]
        public ActionResult CreateBankOperation(BankOperation operation, FormCollection form)
        {
            return CreateOperation(operation, form);
        }

        public ActionResult CreateCardOperation()
        {
            ViewBag.Accounts = AccountService.GetAccounts(_factory, User.Identity.GetUserId())
                .Select(t => new SelectListItem { Value = t.Item1, Text = t.Item2 });
            return View();
        }

        [HttpPost]
        public ActionResult CreateCardOperation(CardOperation operation, FormCollection form)
        {
            return CreateOperation(operation, form);
        }

        public ActionResult CreateMobileOperation()
        {
            ViewBag.Accounts = AccountService.GetAccounts(_factory, User.Identity.GetUserId())
                .Select(t => new SelectListItem { Value = t.Item1, Text = t.Item2 });
            return View();
        }

        [HttpPost]
        public ActionResult CreateMobileOperation(MobileOperation operation, FormCollection form)
        {
            return CreateOperation(operation, form);
        }

        public ActionResult CreateReplenishment()
        {
            ViewBag.Accounts = AccountService.GetAccounts(_factory, User.Identity.GetUserId())
                .Select(t => new SelectListItem { Value = t.Item1, Text = t.Item2 });
            return View();
        }

        [HttpPost]
        public ActionResult CreateReplenishment(MobileOperation operation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PaymentService.ReplenishAccount(operation, _factory, User.Identity.GetUserId());
                    TempData["SuccessMessage"] = "Платеж успешно осуществен.";
                    return RedirectToAction("Index", "Home");
                }
                catch (ValidationException e)
                {
                    ModelState.AddModelError(e.Property, e.Message);
                }
            }
            ViewBag.Accounts = AccountService.GetAccounts(_factory, User.Identity.GetUserId())
                .Select(t => new SelectListItem { Value = t.Item1, Text = t.Item2 });
            return View(operation);
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
            var operation = OperationService.GetOperation(_factory, id);
            if(operation==null)
                return new HttpNotFoundResult();
            return View(operation.GetType().Name.Split('_')[0], operation);
        }

        public ActionResult Cancel(long id = 0)
        {
            try
            {
                PaymentService.CancelPreparedPayment(id, User.Identity.GetUserId(), _factory);
                return Content("Операция успешно отменена.");
                //return RedirectToAction("PreparedPayments");
            }
            catch (ValidationException e)
            {
                return new HttpNotFoundResult(e.Message);
            }
        }

        public ActionResult Accept(long id = 0)
        {
            try
            {
                PaymentService.AcceptPreparedPayment(id, User.Identity.GetUserId(), _factory);
                return Content("Операция успешно подтверждена.");
            }
            catch (ValidationException)
            {
                return new HttpNotFoundResult();
            }
        }
    }
}