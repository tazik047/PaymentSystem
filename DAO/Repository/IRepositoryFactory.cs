using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Owin;

namespace DAO.Repository
{
    public interface IRepositoryFactory
    {
        IAccountRepository AccountRepository { get; }
        ICardRepository CardRepository { get; }
        IMessageRepository MessageRepository { get; }
        IOperationRepository OperationRepository { get; }
        IRequestRepository RequestRepository { get; }
        IUserRepository GetUserRepository(UserManager<User> userManager);
        

        void ConfigAuthorization(IAppBuilder app);
    }
}
