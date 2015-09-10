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
        //todo: Need find only user's account.
        private static List<Account> OrderAccountsByField<T>(IAccountRepository repository, SortType order, Func<Account, T> field)
        {
            var result = repository.Get().OrderBy(field);
            return order == SortType.Descending ? result.Reverse().ToList() : result.ToList();
        }

        public static List<Account> OrderAccountsByName(IAccountRepository repository, SortType order)
        {
            return OrderAccountsByField(repository, order, a => a.Card.Name);
        }

        public static List<Account> OrderAccountsByNumber(IAccountRepository repository, SortType order)
        {
            return OrderAccountsByField(repository, order, a => a.Card.Number);
        }

        public static List<Account> OrderAccountsByBalance(IAccountRepository repository, SortType order)
        {
            return OrderAccountsByField(repository, order, a => a.Balance);
        }

        public static object GetAllAccounts(IRepositoryFactory factory, string userId)
        {
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

        public static Account GetAccount(IRepositoryFactory factory, long id, string userId, bool needCheckUserId)
        {
            var account = factory.AccountRepository.FindById(id);
            if ((account == null) || (needCheckUserId && account.UserId != userId))
                return null;
            return account;
        }
    }
}
