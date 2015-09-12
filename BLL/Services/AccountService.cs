using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Model;
using DAO.Repository;

namespace BLL.Services
{
    public static class AccountService
    {
        public static object GetAllAccounts(IRepositoryFactory factory, string userId, string originId, bool needCheck)
        {
            if (needCheck && originId != userId)
                return null;
            return factory.AccountRepository.Find(a => a.UserId.Equals(userId))
                .Select(a => new
                {
                    Id = a.Card.CardId,
                    a.Card.Name,
                    CreationDate = a.CreationDate.ToShortDateString(),
                    a.Balance,
                    a.IsBlocked
                });
        }

        public static List<Tuple<string, string>> GetAccounts(IRepositoryFactory factory, string userId)
        {
            return factory.AccountRepository.Find(a => a.UserId.Equals(userId))
                .Select(a => new Tuple<string, string>(a.AccountId.ToString(), FormatAccountName(a)))
                .ToList();
        }

        public static string FormatAccountName(Account a)
        {
            return string.Format("{0} - {1} (**{2})", a.Card.Name, a.Balance,
                a.Card.Number.Substring(a.Card.Name.Length - 4));
        }

        public static object GetBlockedAccounts(IRepositoryFactory factory)
        {
            return factory.AccountRepository.Find(a => a.IsBlocked).Select(a => new
            {
                Id = a.Card.CardId,
                FIO = a.User.LastName + " " + a.User.FirstName,
                a.Card.Name,
                CreationDate = a.CreationDate.ToShortDateString(),
                a.Balance,
            });
        }

        public static Account GetAccount(IRepositoryFactory factory, long id, string userId, bool needCheckUserId)
        {
            var account = factory.AccountRepository.FindById(id);
            if ((account == null) || (needCheckUserId && account.UserId != userId))
                return null;
            return account;
        }

        public static void BlockAccount(IRepositoryFactory factory, long accountId, string userId, bool needCheckUserId)
        {
            var account = factory.AccountRepository.FindById(accountId);
            if ((account == null) || (needCheckUserId && account.UserId != userId))
                throw new ValidationException("У вас нет прав для блокировки данного счета.");
            account.IsBlocked = true;
            factory.AccountRepository.Edit(account);
        }

        public static void UnBlockAccount(IRepositoryFactory factory, long accountId)
        {
            var account = factory.AccountRepository.FindById(accountId);
            if (account == null) return;
            account.IsBlocked = false;
            factory.AccountRepository.Edit(account);
        }
    }
}
