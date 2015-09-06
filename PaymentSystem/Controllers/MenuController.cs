using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaymentSystem.Models;

namespace PaymentSystem.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            var items = new []
            {
                new MenuItemViewModel("Главная", Url.Action("Index", new { Controller = "Home", area = "" })),
                new MenuItemViewModel("Мои операции", "", 
                    new MenuItemViewModel("Все операции", Url.Action("Index",new{Controller="Operation", area=""})),
                    new MenuItemViewModel("Пополнения счета",Url.Action("Replenishment",new{Controller="Operation", area=""})),
                    new MenuItemViewModel("Платежи",Url.Action("Payment",new{Controller="Operation", area=""})),
                    new MenuItemViewModel("Подготовленные платежи", Url.Action("Prepared",new{Controller="Operation", area=""}))
                ),
            };
            return View("_Menu",items);
        }
    }
}