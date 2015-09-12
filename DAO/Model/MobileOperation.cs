using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Model
{
    public class MobileOperation : Operation
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }
    }
}
