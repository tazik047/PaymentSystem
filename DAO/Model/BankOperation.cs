using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Model
{
    public class BankOperation : Operation
    {
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DisplayName("Номер счета получателя")]
        public string ResipientNumber { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [MinLength(7, ErrorMessage = "Длина кода должна быть больше 7 символов")]
        [MaxLength(9, ErrorMessage = "Длина кода не должна превышать 9 символов")]
        [RegularExpression("[0-9]+",ErrorMessage = "Код должен быть числом")]
        [DisplayName("Код ЕГРПОУ (ОКПО)")]
        public string BancCode { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [Range(100000, 999999, ErrorMessage = "МФО должен принадлжать диапазону чисел от 100000 до 999999")]
        [DisplayName("МФО")]
        public long SortCode { get; set; }
        [DisplayName("Комментарий")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
    }
}
