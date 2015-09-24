using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Model
{
    public class Message
    {
        public long MessageId { get; set; }
        [DisplayName("Дата")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime Date { get; set; }
        [DisplayName("Тема")]
        public string Theme { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DataType(DataType.MultilineText)]
        [DisplayName("Сообщение")]
        public string Body { get; set; }
        public string FromId { get; set; }
        [DisplayName("Отправитель")]
        public virtual User From { get; set; }
        public string ToId { get; set; }
        [DisplayName("Получатель")]
        public virtual User To { get; set; }
    }
}
