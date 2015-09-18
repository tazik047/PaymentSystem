using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentSystem.Util
{
    public static class MultilineHelper
    {
        public static string PrepareForMultyLine(this string s)
        {
            return s.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\n", "<br/>");
        }
    }
}