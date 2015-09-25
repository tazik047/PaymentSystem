using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAO.Model
{
    [MetadataType(typeof(UserMetaData))]
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DisplayName("Имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        public byte[] ImageBytes { get; set; }
        public string ImgMimeType { get; set; }
        
        [DisplayName("Счета")]
        public virtual ICollection<Account> Accounts { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class UserMetaData
    {
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }
        [Required(ErrorMessage = "Это поле обязательно к заполнению")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\(\d\d\d\) \d\d\d-\d\d-\d\d", ErrorMessage = "Номер телефона должен иметь формат: (095) 111-11-11")]
        [DisplayName("Номер телефона")]
        public virtual string PhoneNumber { get; set; }
    }
}
