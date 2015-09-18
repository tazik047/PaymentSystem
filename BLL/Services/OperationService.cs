using DAO.Model;
using DAO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public static class OperationService
    {
        public static object Operations(IRepositoryFactory factory, long accountId, string userId, bool needCheck)
        {
            var account = factory.AccountRepository.FindById(accountId);
            if (account == null || (needCheck && account.UserId != userId))
                return null;
            return account.Operations.Select(o => new
            {
                OperationDate = o.OperationDate.ToShortDateString(),
                o.Amount,
                Type = o.Type.GetDescription(),
                Id = o.OperationId
            }).ToList();
        }

        public static object AllUserOperations(IRepositoryFactory factory, string id, Func<Operation, bool> predicat)
        {
            var accounts = factory.AccountRepository.Find(a => a.User.Id.Equals(id));
            return accounts.SelectMany(a => a.Operations).Where(predicat).Select(o => new
            {
                o.Account.Card.Name,
                OperationDate = o.OperationDate.ToShortDateString(),
                o.Amount,
                Type = o.Type.GetDescription(),
                Id = o.OperationId
            });
        }
        public static object AllUserOperations(IRepositoryFactory factory, string id)
        {
            return AllUserOperations(factory, id, o => true);
        }

        public static Operation GetOperation(IRepositoryFactory factory, long id)
        {
            return factory.OperationRepository.FindById(id);
        }
    }
}