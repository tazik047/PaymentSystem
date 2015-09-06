using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaymentSystem.Controllers
{
    public class OperationController : Controller
    {
        // GET: Operation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Prepared()
        {
            return View();
        }

        public ActionResult Payment()
        {
            return View();
        }

        public ActionResult Replenishment()
        {
            return View();
        }
    }
}