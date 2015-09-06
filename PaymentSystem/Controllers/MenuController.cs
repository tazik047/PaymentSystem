﻿using System;
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
        public PartialViewResult Index()
        {
            MenuItemViewModel[] items;
            if (User.IsInRole("Admin"))
            {
                items = new[]
                {
                    new MenuItemViewModel("Главная", Url.Action("Index", new {Controller = "Home", area = ""}),
                        "fa-home"),
                    new MenuItemViewModel("Пользователи", "", "fa-users",
                        new MenuItemViewModel("Все пользователи",
                            Url.Action("AllUsers", new {Controller = "Account", area = ""}), ""),
                        new MenuItemViewModel("Заблокированные",
                            Url.Action("AllBlockedUsers", new {Controller = "Account", area = ""}), "")
                        ),
                    new MenuItemViewModel("Заблокированные счета",
                        Url.Action("Block", new {Controller = "Card", area = ""}), "fa-ban"),
                };
            }
            else
            {
                items = new[]
                {
                    new MenuItemViewModel("Главная", Url.Action("Index", new {Controller = "Home", area = ""}),
                        "fa-home"),
                    new MenuItemViewModel("Операции", "", "fa-dashboard",
                        new MenuItemViewModel("Пополненить счет",
                            Url.Action("CreateReplenishment", new {Controller = "Operation", area = ""}), ""),
                        new MenuItemViewModel("Оплатить",
                            Url.Action("CreatePayment", new {Controller = "Operation", area = ""}), ""),
                        new MenuItemViewModel("Подготовить платеж",
                            Url.Action("CreatePreparedPayment", new {Controller = "Operation", area = ""}), "")
                        ),
                    new MenuItemViewModel("Мои счета", "", "fa-credit-card",
                        new MenuItemViewModel("Все карты", Url.Action("Index", new {Controller = "Card", area = ""}), ""),
                        new MenuItemViewModel("Добавить карту",
                            Url.Action("Create", new {Controller = "Card", area = ""}), "")
                        ),
                    new MenuItemViewModel("История операций", "", "fa-history",
                        new MenuItemViewModel("Все операции",
                            Url.Action("Index", new {Controller = "Operation", area = ""}), ""),
                        new MenuItemViewModel("Пополнения",
                            Url.Action("Replenishments", new {Controller = "Operation", area = ""}), ""),
                        new MenuItemViewModel("Платежи",
                            Url.Action("Payments", new {Controller = "Operation", area = ""}), ""),
                        new MenuItemViewModel("Подготовленные платежи",
                            Url.Action("PreparedPayments", new {Controller = "Operation", area = ""}), "")
                        ),
                };
            }
            return PartialView("_Menu", items);
        }
    }
}