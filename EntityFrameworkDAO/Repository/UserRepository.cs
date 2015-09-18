using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Repository;
using DAO.Model;
using EntityFrameworkDAO.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace EntityFrameworkDAO.Repository
{
    class UserRepository : IUserRepository
    {
        private readonly PaymentDbContext _db;
        private readonly ApplicationUserManager _userManager;

        public UserRepository(PaymentDbContext context, 
            ApplicationUserManager userManager)
        {
            _db = context;
            _userManager = userManager;
        }

        public void Add(User item)
        {
            _userManager.Create(item);
        }

        public void Edit(User item, string userId)
        {
            var old = FindById(userId);
            old.LastName = item.LastName;
            old.FirstName = item.FirstName;
            old.PhoneNumber = item.PhoneNumber;
            if (item.ImageBytes != null)
            {
                old.ImageBytes = item.ImageBytes;
                old.ImgMimeType = item.ImgMimeType;
            }
            //item.Id = userId;
            //_userManager.Update(item);
            _db.Entry(old).State = EntityState.Modified;
            _db.SaveChanges();
            //_userManager.
        }

        public void Delete(long id)
        {
            throw new NotSupportedException();
        }

        public void Delete(string id)
        {
            _userManager.Delete(FindById(id));
        }

        public List<User> Get()
        {
            return _db.Users.ToList();
        }

        public List<User> Get(int skip, int take)
        {
            return Get().Skip(skip).Take(take).ToList();
        }

        public List<User> Get(string roleName)
        {
            var role = _db.Roles.FirstOrDefault(r => r.Name == roleName);
            if (role == null) 
                return new List<User>();
            return Get().Where(u => u.Roles.Any(r => r.RoleId == role.Id)).ToList();
        }

        public User FindById(long id)
        {
            throw new NotSupportedException();
        }

        public User FindById(string id)
        {
            return _db.Users.Find(id);
        }

        public List<User> Find(Func<User, bool> predicate)
        {
            return Get().Where(predicate).ToList();
        }

        public void Add(User item, string password, string role = "User")
        {
            _userManager.Create(item, password);
            _userManager.AddToRole(item.Id, role);
        }
        
        public void SetLock(string id, bool block)
        {
            _userManager.SetLockoutEnabled(id, block);
        }


        public void Edit(User item)
        {
            _userManager.Update(item);
        }
    }
}
