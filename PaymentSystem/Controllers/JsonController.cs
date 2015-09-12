using BLL.Services;
using DAO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityFrameworkDAO.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
            var operations = OperationServices.Operations(_factory, id, User.Identity.GetUserId(), User.IsInRole("User"));
            if (operations == null)
                return new HttpNotFoundResult();
            return Json(operations, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Accounts(string id)
        {
            var accounts = AccountService.GetAllAccounts(_factory, id, User.Identity.GetUserId(), User.IsInRole("User"));
            if (accounts == null)
                return new HttpNotFoundResult();
            return Json(accounts, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Support")]
        public ActionResult Users()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var users = userManager.Users.Select(u => new
            {
                isBlocked = u.LockoutEnabled,
                u.Id,
                u.Email,
                u.LastName,
                u.FirstName
            }).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin, Support")]
        public ActionResult BlockedUsers()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var users = userManager.Users.Where(u=>u.LockoutEnabled).Select(u => new
            {
                isBlocked = u.LockoutEnabled,
                u.Id,
                u.Email,
                u.LastName,
                u.FirstName
            }).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}