using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Repository
{
    public interface IRepositoryFactory
    {
        IAccountRepository AccountRepository { get; }
        ICardRepository CardRepository { get; }
        IOperationRepository OperationRepository { get; }
    }
}
