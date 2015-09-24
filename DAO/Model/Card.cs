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
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [MinLength(14, ErrorMessage = "Длина номера карты должна быть больше 14 символов")]
        [MaxLength(23, ErrorMessage = "Длина номера карты не должна превышать 23 символов")]
        [RegularExpression(@"[\d\s]+", ErrorMessage = "Номер карты имеет неверный формат.")]
        [DisplayName("Номер карты")]
        public string Number { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DisplayName("Полное имя")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [RegularExpression(@"\d\d / \d{4}",ErrorMessage = "Дата должна иметь формат мм/гггг")]
        [CustomValidation(typeof(MyValidation),"ValidateExpirationDate", ErrorMessage = "Дата должна быть больше текущего числа.")]
        [DisplayName("Срок действия")]
        public string ExpirationDate { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [Range(100, 9999, ErrorMessage = "Код должен быть в диапазоне чисел от 100 до 9999")]
        [DisplayName("CVC код")]
        public int Cvc { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DisplayName("Название карты")]
        public string Name { get; set; }

        public long AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
