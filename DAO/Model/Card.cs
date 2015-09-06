using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Model
{
    public class Card
    {
        public long CardId { get; set; }
        public string Number { get; set; }
        public string FullName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Cvc { get; set; }
        public string Name { get; set; }

        public long AccountId { get; set; }
        public virtual Account Accounts { get; set; } 
    }
}
