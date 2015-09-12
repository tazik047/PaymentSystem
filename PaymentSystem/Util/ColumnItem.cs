using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaymentSystem.Util
{
    public class ColumnItem
    {
        public string Name { get; set; }
        public MvcHtmlString Title { get; set; }
        public ColumnItem(string name, MvcHtmlString title)
        {
            Name = name;
            Title = title;
        }

        public ColumnItem(string name, string title) : this(name, new MvcHtmlString(title)) { }
    }
}