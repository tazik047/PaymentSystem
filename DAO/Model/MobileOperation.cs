﻿using System;
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
        [RegularExpression(@"\(\d\d\d\) \d\d\d-\d\d-\d\d",ErrorMessage = "Номер телефона должен иметь формат: (095) 111-11-11")]
        public string MobileNumber { get; set; }
    }
}
