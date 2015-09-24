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
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [MinLength(14, ErrorMessage = "Длина номера карты должна быть больше 14 символов")]
        [MaxLength(23, ErrorMessage = "Длина номера карты не должна превышать 23 символов")]
        [RegularExpression(@"[\d\s]+", ErrorMessage = "Номер карты имеет неверный формат.")]
        [DisplayName("Номер карты")]
        public string CardNumber { get; set; }
    }
}
