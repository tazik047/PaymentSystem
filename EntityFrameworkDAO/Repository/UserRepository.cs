using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Repository;
using DAO.Model;
using Microsoft.AspNet.Identity;

namespace EntityFrameworkDAO.Repository
{
    class UserRepository : IUserRepository
    {
        public UserManager<User> UserManager
        {
            get { throw new NotImplementedException(); }
        }

        public Microsoft.AspNet.Identity.Owin.SignInManager<User, string> SignInManager
        {
            get { throw new NotImplementedException(); }
        }

        public void Add(User item)
        {
            throw new NotImplementedException();
        }

        public void Edit(User item)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public List<User> Get()
        {
            throw new NotImplementedException();
        }

        public List<User> Get(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public User FindById(long id)
        {
            throw new NotImplementedException();
        }

        public List<User> Find(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
