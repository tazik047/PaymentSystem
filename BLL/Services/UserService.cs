using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Model;
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
            var user = manager.FindById(id);
            return user == null || user.LockoutEnabled;
        }
    }
}
