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

        public ActionResult Operations(long id)
        {
            var operations = OperationServices.Operations(_factory, id, User.Identity.GetUserId(), User.IsInRole("User"));
            if (operations == null)
                return new HttpNotFoundResult();
            return Json(operations, JsonRequestBehavior.AllowGet);
        }
    }
}