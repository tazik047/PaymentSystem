using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DAO.Model
{
    public class Operation
    {
        public long OperationId { get; set; }
        [DisplayName("Дата оперцаии")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime OperationDate { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [Range(0.01, Double.MaxValue, ErrorMessage = "Сумма должна быть больше одной копейки")]
        [DisplayName("Сумма")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d+(.\d\d)?$",ErrorMessage="Сумма должна иметь формат 100.01")]
        public decimal Amount { get; set; }
        [DisplayName("Тип")]
        public OperationType Type { get; set; }
        [DisplayName("Использовать счет")]
        public long AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
