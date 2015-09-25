using BLL.Services;
using DAO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAO.Model;
using EntityFrameworkDAO.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Text;
//using Microsoft.Owin.Security;

namespace PaymentSystem.Controllers
{
    public class JsonController : Controller
    {
        private readonly IRepositoryFactory _factory;
        public JsonController(IRepositoryFactory factory)
        {
            _factory = factory;
        }

        public ActionResult Operations(long id = 0)
        {
            return PrepareResult(OperationService.Operations(_factory, id, User.Identity.GetUserId(), User.IsInRole("User")));
        }

        public ActionResult AllOperations()
        {
            return PrepareResult(OperationService.AllUserOperations(_factory, User.Identity.GetUserId()));
        }

        public ActionResult PreparedOperations()
        {
            return PrepareResult(OperationService.AllUserOperations(_factory,
                User.Identity.GetUserId(),
                o => o.Type == OperationType.PreparedPayment));
        }

        public ActionResult ReplenishmentOperations()
        {
            return PrepareResult(OperationService.AllUserOperations(_factory,
                User.Identity.GetUserId(),
                o => o.Type == OperationType.Replenishment));
        }

        public ActionResult PaymnetOperations()
        {
            return PrepareResult(OperationService.AllUserOperations(_factory,
                User.Identity.GetUserId(),
                o => o.Type == OperationType.Paymnet));
        }

        public ActionResult Accounts(string id)
        {
            if (string.IsNullOrEmpty(id))
                id = User.Identity.GetUserId();
            return PrepareResult(AccountService.GetAllAccounts(_factory, id, User.Identity.GetUserId(), User.IsInRole("User")));
        }

        [Authorize(Roles = "Admin, Support")]
        public ActionResult Users()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return PrepareResult(UserService.GetUnBlockedUsers(userManager));
        }

        [Authorize(Roles = "Admin, Support")]
        public ActionResult BlockedUsers()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return PrepareResult(UserService.GetBlockedUsers(userManager));
        }

        public ActionResult BlockedAccounts()
        {
            return PrepareResult(AccountService.GetBlockedAccounts(_factory));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Requests()
        {
            return PrepareResult(RequestService.Requests(_factory));
        }

        public ActionResult Inbox()
        {
            return PrepareResult(MessageService.GetInbox(_factory, User.Identity.GetUserId()));
        }

        public ActionResult Outbox()
        {
            return PrepareResult(MessageService.GetOutbox(_factory, User.Identity.GetUserId()));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UserOperationsReport()
        {
            var res = ReportService.UserOperations(_factory);
            return PrepareResult(res
                .Select(r => new
                {
                    r.Item1.FirstName,
                    r.Item1.LastName,
                    r.Item1.Id,
                    Accounts = prepareArray(r.Item2),
                    Sum = prepareArray("Платежи: " + r.Item3 + " грн.", "Пополнения: " + r.Item4 + " грн.")
                }));
        }

        private string prepareArray<T>(params T[] items)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var i in items)
                sb.Append(i + "<br />");
            return sb.ToString();
        }

        private ActionResult PrepareResult(object o)
        {
            if (o == null)
                return new HttpNotFoundResult();
            return Json(o, JsonRequestBehavior.AllowGet);
        }
    }
}