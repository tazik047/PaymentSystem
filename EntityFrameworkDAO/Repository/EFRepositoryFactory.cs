using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Model;
using DAO.Repository;
using EntityFrameworkDAO.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Owin;

namespace EntityFrameworkDAO.Repository
{
    public class EFRepositoryFactory : IRepositoryFactory
    {
        private readonly PaymentDbContext _context;

        public IAccountRepository AccountRepository
        {
            get
            {
                return new AccountRepository(_context);
            }
        }

        public ICardRepository CardRepository
        {
            get
            {
                return new CardRepository(_context);
            }
        }

        public IOperationRepository OperationRepository
        {
            get
            {
                return new OperationRepository(_context);
            }
        }

        public IRequestRepository RequestRepository
        {
            get { return new RequestRepository(_context); }
        }

        public IUserRepository GetUserRepository(UserManager<User> userManager)
        {
            return new UserRepository(_context, (ApplicationUserManager) userManager);
        }

        public EFRepositoryFactory(string connectionName)
        {
            _context = new PaymentDbContext(connectionName);
        }


        public void ConfigAuthorization(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => _context);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
        }




        public IMessageRepository MessageRepository
        {
            get { return new MessageRepository(_context); }
        }
    }
}
