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
    /// <summary>
    /// Class for manage users.
    /// </summary>
    public static class UserService
    {
        /// <summary>
        /// Method for block/unblock user
        /// </summary>
        /// <param name="id">id of user for blocking</param>
        /// <param name="lockUser">True for block user. False for unblock</param>
        public static void LockUser(UserManager<User> manager, string id, bool lockUser)
        {
            var user = manager.FindById(id);
            user.LockoutEndDateUtc = lockUser ? DateTime.MaxValue : DateTime.Now;
            user.LockoutEnabled = lockUser;
            manager.Update(user);
        }

        /// <summary>
        /// Get avatar for selected user
        /// </summary>
        /// <param name="id">Id of selected user</param>
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

        public static object GetUnBlockedUsers(UserManager<User> manager)
        {
            return manager.Users.Select(u => new
            {
                isBlocked = u.LockoutEnabled,
                u.Id,
                u.Email,
                u.LastName,
                u.FirstName
            }).ToList();
        }

        public static object GetBlockedUsers(UserManager<User> manager)
        {
            return manager.Users.Where(u => u.LockoutEnabled).Select(u => new
            {
                isBlocked = u.LockoutEnabled,
                u.Id,
                u.Email,
                u.LastName,
                u.FirstName
            }).ToList();
        }
    }
}
