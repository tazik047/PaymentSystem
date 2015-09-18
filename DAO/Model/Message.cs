using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Model
{
    public class Message
    {
        public long MessageId { get; set; }
        public DateTime Date { get; set; }
        public string Theme { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        public string FromId { get; set; }
        public virtual User From { get; set; }
        public string ToId { get; set; }
        public virtual User To { get; set; }
    }
}
