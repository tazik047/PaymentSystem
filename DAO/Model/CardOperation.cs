using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Model
{
    public class CardOperation :Operation
    {
        [Required]
        [MinLength(14)]
        [MaxLength(23)]
        [RegularExpression(@"[\d\s]+", ErrorMessage = "Номер карты имеет неверный формат.")]
        [DisplayName("Номер карты")]
        public string CardNumber { get; set; }
    }
}
