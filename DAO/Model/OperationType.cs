using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Model
{
    public enum OperationType
    {
        [Display(Name = "Пополнение")]
        Replenishment,
        [Display(Name = "Платеж")]
        Paymnet,
        [Display(Name = "Отложенный платеж")]
        PreparedPayment
    }
}
