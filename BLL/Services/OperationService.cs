using DAO.Model;
using DAO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// Class for working with operations.
    /// </summary>
    public static class OperationService
    {
        /// <summary>
        /// Method for get all operations of selected account
        /// </summary>
        /// <param name="accountId">Account which contains selected operations</param>
        /// <param name="userId">Current user id</param>
        /// <param name="needCheck">Check if operation should be created by current user</param>
        public static object Operations(IRepositoryFactory factory, long accountId, string userId, bool needCheck)
        {
            var account = factory.AccountRepository.FindById(accountId);
            if (account == null || (needCheck && account.UserId != userId))
                return null;
            return account.Operations.Select(o => new
            {
                OperationDate = o.OperationDate.ToString("dd.MM.yyyy"),
                Amount = o.Amount + " грн.",
                Type = o.Type.GetDescription(),
                Id = o.OperationId
            }).ToList();
        }

        /// <summary>
        /// Get all operation of current user.
        /// </summary>
        /// <param name="id">Current user id</param>
        /// <param name="predicat">Filter of selected operations</param>
        public static object AllUserOperations(IRepositoryFactory factory, string id, Func<Operation, bool> predicat)
        {
            var accounts = factory.AccountRepository.Find(a => a.User.Id.Equals(id));
            return accounts.SelectMany(a => a.Operations).Where(predicat).Select(o => new
            {
                o.Account.Card.Name,
                OperationDate = o.OperationDate.ToString("dd.MM.yyyy"),
                Amount = o.Amount + " грн.",
                Type = o.Type.GetDescription(),
                Id = o.OperationId
            });
        }

        /// <summary>
        /// Get all operation of current user.
        /// </summary>
        /// <param name="id">Current user id</param>
        public static object AllUserOperations(IRepositoryFactory factory, string id)
        {
            return AllUserOperations(factory, id, o => true);
        }

        /// <summary>
        /// Get selected operation by id.
        /// </summary>
        /// <param name="id">Id of operation</param>
        public static Operation GetOperation(IRepositoryFactory factory, long id)
        {
            return factory.OperationRepository.FindById(id);
        }
    }
}