using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Model
{
    public class Request
    {
        public long RequestId { get; set; }
        [DisplayName("Дата")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Date { get; set; }
        public long AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
