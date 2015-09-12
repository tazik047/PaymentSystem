using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Model
{
    public class CardOperation :Operation
    {
        [Required]
        [MinLength(12)]
        [MaxLength(19)]
        public string CardNumber { get; set; }
    }
}
