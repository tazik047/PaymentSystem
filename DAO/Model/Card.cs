using System;
using System.Collections.Generic;
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
        [MinLength(12)]
        [MaxLength(19)]
        [RegularExpression(@"[\d\s]+", ErrorMessage = "Номер карты имеет неверный формат.")]
        public string Number { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [RegularExpression(@"\d\d / \d{4}",ErrorMessage = "Дата должна иметь формат мм/гггг")]
        [CustomValidation(typeof(MyValidation),"ValidateExpirationDate", ErrorMessage = "Дата должна быть больше текущего числа.")]
        public string ExpirationDate { get; set; }
        [Required]
        [Range(100, 9999)]
        public int Cvc { get; set; }
        [Required]
        public string Name { get; set; }

        public long AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
