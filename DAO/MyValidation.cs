using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class MyValidation
    {
        public static ValidationResult ValidateExpirationDate(string date)
        {
            try
            {
                var mas = date.Split('/').Select(i => Convert.ToInt32(i)).ToList();
                var expirationDate = new DateTime(mas[1], mas[0], 1);
                if(expirationDate>DateTime.Now)
                    return ValidationResult.Success;
                return new ValidationResult("Карта недействительна");
            }
            catch
            {
                return new ValidationResult("Дата имеет неверный формат");
            }
        }
    }
}
