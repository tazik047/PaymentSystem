using System.Collections.Generic;
using DAO.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace DAO.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        void Add(User item, string password, string role="User");
        void Edit(User item, string userId);
        void Delete(string id);
        User FindById(string id);
        List<User> Get(string role);
        void SetLock(string id, bool block);
    }
}
