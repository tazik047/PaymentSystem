using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Services;
using DAO.Repository;

namespace PaymentSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RequestController : Controller
    {
        private readonly IRepositoryFactory _factory;

        public RequestController(IRepositoryFactory factory)
        {
            _factory = factory;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Last()
        {
            ViewBag.Count = RequestService.CountRequest(_factory);
            return PartialView(RequestService.FirstNRequests(_factory, 5));
        }
    }
}