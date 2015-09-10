using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BLL
{
    static class EnumExtension
    {
        public static string GetDescription(this Enum e)
        {
            var field = e.GetType().GetField(e.ToString());
            var attributes = (DisplayAttribute[])field.GetCustomAttributes(typeof(DisplayAttribute), false);
            return attributes.Length > 0 ? attributes[0].Name : field.Name;
        }
    }
}
