using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Model
{
    public class Card
    {
        public long CardId { get; set; }
        [Required]
        [MinLength(12)]
        [MaxLength(19)]
        public string Number { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        [Range(100, 9999)]
        public int Cvc { get; set; }
        [Required]
        public string Name { get; set; }

        public long AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
