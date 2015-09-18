﻿using System;
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
        public DateTime OperationDate { get; set; }
        [Required]
        [Range(0, Double.MaxValue)]
        [DisplayName("Сумма")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        [DisplayName("Тип")]
        public OperationType Type { get; set; }
        [DisplayName("Использовать счет")]
        public long AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
