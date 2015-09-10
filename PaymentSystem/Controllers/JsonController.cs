using BLL.Services;
using DAO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

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
            var operations = OperationServices.Operations(_factory, id, User.Identity.GetUserId(), User.IsInRole("User"));
            if (operations == null)
                return new HttpNotFoundResult();
            return Json(operations, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Accounts(long userId = 0)
        {
            var accounts = AccountService.GetAllAccounts(_factory, User.Identity.GetUserId());
            if (accounts == null)
                return new HttpNotFoundResult();
            return Json(accounts, JsonRequestBehavior.AllowGet);
        }
    }
}