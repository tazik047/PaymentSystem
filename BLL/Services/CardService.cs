using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Model;
using DAO.Repository;

namespace BLL.Services
{
    public static class CardService
    {
        public static List<Card> GetAllCards(IRepositoryFactory factory, string userId)
        {
            return factory.CardRepository.Find(c => c.Account.UserId.Equals(userId));
        } 
    }
}
