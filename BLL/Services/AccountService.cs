using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Model;
using DAO.Repository;

namespace BLL.Services
{
    /// <summary>
    /// Class for work with account user's.
    /// </summary>
    public static class AccountService
    {
        /// <summary>
        /// Method for getting all user accounts.
        /// </summary>
        /// <param name="factory">Factory for working with data</param>
        /// <param name="userId">Id of user which you want get</param>
        /// <param name="originId">Id of current user</param>
        /// <param name="needCheck">true if userId shoukd equals to originId</param>
        /// <returns>List of accounts</returns>
        public static object GetAllAccounts(IRepositoryFactory factory, string userId, string originId, bool needCheck)
        {
            if (needCheck && originId != userId)
                return null;
            return factory.AccountRepository.Find(a => a.UserId.Equals(userId))
                .Select(a => new
                {
                    Id = a.Card.CardId,
                    a.Card.Name,
                    CreationDate = a.CreationDate.ToString("dd.MM.yyyy"),
                    Balance = a.Balance + " грн.",
                    a.IsBlocked
                });
        }

        /// <summary>
        /// Method for find all not blocked accounts and formatting them for display.
        /// </summary>
        public static List<Tuple<string, string>> GetAccounts(IRepositoryFactory factory, string userId)
        {
            return factory.AccountRepository.Find(a => a.UserId.Equals(userId) && !a.IsBlocked)
                .Select(a => new Tuple<string, string>(a.AccountId.ToString(), FormatAccountName(a)))
                .ToList();
        }

        private static string FormatAccountName(Account a)
        {
            return string.Format("{0} - {1} грн. (**{2})", a.Card.Name, a.Balance,
                a.Card.Number.Substring(a.Card.Number.Length - 4));
        }

        /// <summary>
        /// Method for getting all blocked accounts.
        /// </summary>
        public static object GetBlockedAccounts(IRepositoryFactory factory)
        {
            return factory.AccountRepository.Find(a => a.IsBlocked).Select(a => new
            {
                Id = a.Card.CardId,
                FIO = a.User.LastName + " " + a.User.FirstName,
                a.Card.Name,
                CreationDate = a.CreationDate.ToString("dd.MM.yyyy"),
                Balance = a.Balance + " грн.",
            });
        }

        /// <summary>
        /// Get account for selected user.
        /// </summary>
        /// <param name="id">Id of account</param>
        /// <param name="userId">Id of current user</param>
        /// <param name="needCheckUserId">Check if account should be created by current user</param>
        /// <returns></returns>
        public static Account GetAccount(IRepositoryFactory factory, long id, string userId, bool needCheckUserId)
        {
            var account = factory.AccountRepository.FindById(id);
            if ((account == null) || (needCheckUserId && account.UserId != userId))
                return null;
            return account;
        }

        /// <summary>
        /// Method for blocking account.
        /// </summary>
        /// <param name="accountId">Id of account</param>
        /// <param name="userId">Id of current user</param>
        /// <param name="needCheckUserId">Check if account should be created by current user</param>
        public static void BlockAccount(IRepositoryFactory factory, long accountId, string userId, bool needCheckUserId)
        {
            var account = factory.AccountRepository.FindById(accountId);
            if ((account == null) || (needCheckUserId && account.UserId != userId))
                throw new ValidationException("У вас нет прав для блокировки данного счета.");
            account.IsBlocked = true;
            factory.AccountRepository.Edit(account);
        }

        /// <summary>
        /// Unblock selected account.
        /// </summary>
        /// <param name="accountId">Id of account which should be unblocked</param>
        public static void UnBlockAccount(IRepositoryFactory factory, long accountId)
        {
            var account = factory.AccountRepository.FindById(accountId);
            if (account == null) return;
            account.IsBlocked = false;
            factory.AccountRepository.Edit(account);
            var request = factory.RequestRepository.Find(r => r.Account.AccountId == accountId).FirstOrDefault();
            if (request != null)
            {
                RequestService.AcceptRequest(request.RequestId, factory);
            }
        }
    }
}
