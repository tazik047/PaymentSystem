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
        [Required]
        [DisplayName("Номер счета получателя")]
        public string ResipientNumber { get; set; }
        [Required]
        [MinLength(7)]
        [MaxLength(9)]
        [RegularExpression("[0-9]+")]
        [DisplayName("Код ЕГРПОУ (ОКПО)")]
        public string BancCode { get; set; }
        [Required]
        [Range(100000, 999999)]
        [DisplayName("МфО")]
        public long SortCode { get; set; }
        [DisplayName("Комментарий")]
        public string Comment { get; set; }
    }
}
