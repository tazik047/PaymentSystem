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
    /// Class for working with card.
    /// </summary>
    public static class CardService
    {
        /// <summary>
        /// Method for creating new card and prepare new account.
        /// </summary>
        /// <param name="card">Card, which need create.</param>
        /// <param name="userId">Id of current user</param>
        public static void CreateCard(Card card, string userId, IRepositoryFactory factory)
        {
            card.Number = new string(card.Number.Where(c=>c!=' ').ToArray());
            var account = new Account
            {
                Balance = 0,
                Card = card,
                CreationDate = DateTime.Now,
                IsBlocked = false,
                UserId = userId
            };
            factory.AccountRepository.Add(account);
        }
    }
}
