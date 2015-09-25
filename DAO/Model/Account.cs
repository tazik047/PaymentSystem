using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DAO.Model
{
    public class Account
    {
        public long AccountId { get; set; }

        [DisplayName("Дата создания")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Остаток")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Balance { get; set; }

        public bool IsBlocked { get; set; }
        public virtual ICollection<Operation> Operations { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public long CardId { get; set; }
        public virtual Card Card { get; set; }

    }
}
