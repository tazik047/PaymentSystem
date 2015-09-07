using DAO.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace DAO.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        UserManager<User> UserManager { get; }
        SignInManager<User, string> SignInManager { get; }
    }
}
