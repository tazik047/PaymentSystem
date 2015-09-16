using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Model;
using DAO.Repository;
using Microsoft.AspNet.Identity;

namespace BLL.Services
{
    public static class UserService
    {
        public static void LockUser(UserManager<User> manager, string id, bool lockUser)
        {
            var user = manager.FindById(id);
            user.LockoutEndDateUtc = lockUser ? DateTime.MaxValue : DateTime.Now;
            user.LockoutEnabled = lockUser;
            manager.Update(user);
        }
        public static bool IsBlocked(UserManager<User> manager, string id)
        {
            var user = manager.Users.FirstOrDefault(u => u.Id.Equals(id));
                //manager.FindById(id);
            return user == null || user.LockoutEnabled;
        }

        public static Tuple<byte[], string> GetImage(IRepositoryFactory factory, UserManager<User> manager, string id)
        {
            var user = factory.GetUserRepository(manager).FindById(id);
            if (user == null || user.ImageBytes == null)
            {
                using (var stream = new MemoryStream())
                {
                    Properties.Resources.defaultImg.Save(stream, ImageFormat.Jpeg);
                    return new Tuple<byte[], string>(stream.ToArray(), "image/jpeg");
                }
            }
            return new Tuple<byte[], string>(user.ImageBytes, user.ImgMimeType);
        }
    }
}
