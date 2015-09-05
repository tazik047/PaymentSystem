using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Model;
using DAO.Repository;

namespace EntityFrameworkDAO.Repository
{
    class CardRepository : ICardRepository
    {
        private readonly PaymentDbContext _db;

        public CardRepository(PaymentDbContext context)
        {
            _db = context;
        }

        public void Add(Card item)
        {
            _db.Cards.Add(item);
            _db.SaveChanges();
        }

        public void Edit(Card item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(long id)
        {
            var item = FindById(id);
            if(item== null) return;
            _db.Cards.Remove(item);
            _db.SaveChanges();
        }

        public List<Card> Get()
        {
            return _db.Cards.ToList();
        }

        public List<Card> Get(int skip, int take)
        {
            return Get().Skip(skip).Take(take).ToList();
        }

        public Card FindById(long id)
        {
            return _db.Cards.Find(id);
        }

        public List<Card> Find(Func<Card, bool> predicate)
        {
            return Get().Where(predicate).ToList();
        }
    }
}
