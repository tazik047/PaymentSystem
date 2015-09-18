using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Model
{
    public class Card
    {
        public long CardId { get; set; }
        [Required]
        [MinLength(14)]
        [MaxLength(23)]
        [RegularExpression(@"[\d\s]+", ErrorMessage = "Номер карты имеет неверный формат.")]
        [DisplayName("Номер карты")]
        public string Number { get; set; }
        [Required]
        [DisplayName("Полное имя")]
        public string FullName { get; set; }
        [Required]
        [RegularExpression(@"\d\d / \d{4}",ErrorMessage = "Дата должна иметь формат мм/гггг")]
        [CustomValidation(typeof(MyValidation),"ValidateExpirationDate", ErrorMessage = "Дата должна быть больше текущего числа.")]
        [DisplayName("Срок действия")]
        public string ExpirationDate { get; set; }
        [Required]
        [Range(100, 9999)]
        [DisplayName("CVC код")]
        public int Cvc { get; set; }
        [Required]
        [DisplayName("Название карты")]
        public string Name { get; set; }

        public long AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
