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
    class AccountRepository : IAccountRepository
    {
        private readonly PaymentDbContext _db;

        public AccountRepository(PaymentDbContext context)
        {
            _db = context;
        }

        public void Add(Account item)
        {
            _db.Accounts.Add(item);
            _db.SaveChanges();
        }

        public void Edit(Account item)
        {
            _db.Entry(item).State = EntityState.Modified;
            /*var old = FindById(item.AccountId);
            old.Balance = item.Balance;
            old.Card = item.Card;
            old.Balance = item.Balance;
            old.IsBlocked = item.IsBlocked;
            old.Operations.Clear();
            foreach (var tag in item.Operations)
                old.Operations.Add(tag);*/
            _db.SaveChanges();
        }

        public void Delete(long id)
        {
            var item = FindById(id);
            if(item== null) return;
            _db.Accounts.Remove(item);
            _db.SaveChanges();
        }

        public List<Account> Get()
        {
            return _db.Accounts.ToList();
        }

        public List<Account> Get(int skip, int take)
        {
            return Get().Skip(skip).Take(take).ToList();
        }

        public Account FindById(long id)
        {
            return _db.Accounts.Find(id);
        }

        public List<Account> Find(Func<Account, bool> predicate)
        {
            return Get().Where(predicate).ToList();
        }
    }
}
