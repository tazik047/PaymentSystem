using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Services;
using DAO.Model;
using DAO.Repository;
using Microsoft.AspNet.Identity;

namespace PaymentSystem.Controllers
{
    public class MessageController : Controller
    {
        private readonly IRepositoryFactory _factory;
        public MessageController(IRepositoryFactory factory)
        {
            _factory = factory;
        }

        public ActionResult Inbox()
        {
            return View();
        }

        public ActionResult Outbox()
        {
            return View();
        }

        public ActionResult Details(long id = 0)
        {
            var message = MessageService.GetMessage(_factory, id);
            if(message==null)
                return new HttpNotFoundResult();
            return View(message);
        }

        [HttpGet]
        public ActionResult Answer(long id = 0)
        {
            if (id == 0)
                return new HttpNotFoundResult();
            var message = MessageService.GetMessage(_factory, id);
            if(message==null)
                return new HttpNotFoundResult();
            message.Body = "";
            message.ToId = message.FromId;
            return View(message);
        }

        [HttpPost]
        public ActionResult Answer(Message message, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                message.FromId = User.Identity.GetUserId();
                MessageService.Answer(_factory, message);
                TempData["SuccessMessage"] = "Сообщение успешно отправлено.";
                return RedirectToAction("Outbox");
            }
            return View(message);
        }

        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                message.FromId = User.Identity.GetUserId();
                MessageService.SendToSupport(_factory, message);
                TempData["SuccessMessage"] = "Сообщение успешно отправлено.";
                return RedirectToAction("Outbox");
            }
            return View(message);
        }
    }
}