using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Model
{
    public class User
    {
        public long UserId { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsBlocked { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
