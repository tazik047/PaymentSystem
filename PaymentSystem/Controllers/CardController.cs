using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaymentSystem.Controllers
{
    public class CardController : Controller
    {
        // GET: Card
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Block(long? cardId)
        {
            return View();
        }

        public ActionResult RequertToUnBlock(long? cardId)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UnBlock(long? cardId)
        {
            return View();
        }
    }
}