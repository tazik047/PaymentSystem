using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentSystem.Models
{
    public class MenuItemViewModel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public MenuItemViewModel[] Items { get; set; }

        public MenuItemViewModel(string title, string link, params MenuItemViewModel[] items)
        {
            Title = title;
            Link = link;
            Items = items;
        }
    }
}