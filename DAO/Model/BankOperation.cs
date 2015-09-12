using System;
using System.Collections.Generic;
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
        public string ResipientNumber { get; set; }
        [Required]
        [MinLength(7)]
        [MaxLength(9)]
        [RegularExpression("[0-9]+")]
        public string BancCode { get; set; }
        [Required]
        [Range(100000, 999999)]
        public long SortCode { get; set; }
        public string Comment { get; set; }
    }
}
