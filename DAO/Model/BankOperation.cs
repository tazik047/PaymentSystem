using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Model
{
    public class BankOperation : Operation
    {
        public string ResipientNumber { get; set; }
        public long BancCode { get; set; }
        public long SortCode { get; set; }
        public string Comment { get; set; }
    }
}
