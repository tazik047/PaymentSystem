using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DAO.Model
{
    public class Operation
    {
        public long OperationId { get; set; }
        public DateTime OperationDate { get; set; }
        [Required]
        [Range(0, Double.MaxValue)]
        public double Amount { get; set; }
        public OperationType Type { get; set; }

        public long AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
