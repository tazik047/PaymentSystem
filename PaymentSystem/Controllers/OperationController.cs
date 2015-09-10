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

        public ActionResult CreatePayment()
        {
            return View();
        }

        public PartialViewResult CreateBankOperation()
        {
            return PartialView();
        }

        public PartialViewResult CreateCardOperation()
        {
            return PartialView();
        }

        public PartialViewResult CreateMobileOperation()
        {
            return PartialView();
        }

        public ActionResult CreateReplenishment()
        {
            return View();
        }

        public ActionResult CreatePreparedPayment()
        {
            return View();
        }

        public ActionResult PreparedPayments()
        {
            return View();
        }

        public ActionResult Payments()
        {
            return View();
        }

        public ActionResult Replenishments()
        {
            return View();
        }

        public ActionResult Details(long id = 0)
        {
            return View();
        }
    }
}