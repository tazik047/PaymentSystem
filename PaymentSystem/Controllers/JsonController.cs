﻿using BLL.Services;
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
            return PrepareResult(OperationServices.Operations(_factory, id, User.Identity.GetUserId(), User.IsInRole("User")));
        }

        public ActionResult AllOperations()
        {
            return PrepareResult(OperationServices.AllUserOperations(_factory, User.Identity.GetUserId()));
        }

        public ActionResult PreparedOperations()
        {
            return PrepareResult(OperationServices.AllUserOperations(_factory,
                User.Identity.GetUserId(),
                o => o.Type == OperationType.PreparedPayment));
        }

        public ActionResult ReplenishmentOperations()
        {
            return PrepareResult(OperationServices.AllUserOperations(_factory,
                User.Identity.GetUserId(),
                o => o.Type == OperationType.Replenishment));
        }

        public ActionResult PaymnetOperations()
        {
            return PrepareResult(OperationServices.AllUserOperations(_factory,
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
            var users = userManager.Users.Where(u => u.LockoutEnabled).Select(u => new
            {
                isBlocked = u.LockoutEnabled,
                u.Id,
                u.Email,
                u.LastName,
                u.FirstName
            }).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BlockedAccounts()
        {
            return PrepareResult(AccountService.GetBlockedAccounts(_factory));
        }

        private ActionResult PrepareResult(object o)
        {
            if (o == null)
                return new HttpNotFoundResult();
            return Json(o, JsonRequestBehavior.AllowGet);
        }
    }
}