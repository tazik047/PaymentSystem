using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAO.Repository;
using BLL.Services;

namespace PaymentSystem.Controllers
{
    public class ReportController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            
            return View();
        }
    }
}