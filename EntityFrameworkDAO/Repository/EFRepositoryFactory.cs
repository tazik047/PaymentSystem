using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Repository;
using EntityFrameworkDAO.Identity;
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
    }
}
