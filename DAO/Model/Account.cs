using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO.Model
{
    public class Account
    {
        public long AccountId { get; set; }
        public DateTime CreationDate { get; set; }
        public float Balance { get; set; }
        public bool IsBlocked { get; set; }
        public ICollection<Operation> Operations { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public long CardId { get; set; }
        public virtual Card Card { get; set; }

    }
}
