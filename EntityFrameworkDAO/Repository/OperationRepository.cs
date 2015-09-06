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
    class OperationRepository : IOperationRepository
    {
        private readonly PaymentDbContext _db;

        public OperationRepository(PaymentDbContext context)
        {
            _db = context;
        }

        public void Add(Operation item)
        {
            _db.Operations.Add(item);
            _db.SaveChanges();
        }

        public void Edit(Operation item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(long id)
        {
            var item = FindById(id);
            if (item == null) return;
            _db.Operations.Remove(item);
            _db.SaveChanges();
        }

        public List<Operation> Get()
        {
            return _db.Operations.ToList();
        }

        public List<Operation> Get(int skip, int take)
        {
            return Get().Skip(skip).Take(take).ToList();
        }

        public Operation FindById(long id)
        {
            return _db.Operations.Find(id);
        }

        public List<Operation> Find(Func<Operation, bool> predicate)
        {
            return Get().Where(predicate).ToList();
        }

        public List<T> Get<T>() where T : Operation
        {
            var currnetType = typeof(T).Name;
            if (currnetType == typeof(BankOperation).Name)
            {
                return _db.BankOperations.Cast<T>().ToList<T>();
            }
            if (currnetType == typeof(CardOperation).Name)
            {
                return _db.CardOperations.Cast<T>().ToList<T>();
            }
            if (currnetType == typeof(MobileOperation).Name)
            {
                return _db.MobileOperations.Cast<T>().ToList<T>();
            }
            if (currnetType == typeof(Operation).Name)
            {
                return _db.Operations.Cast<T>().ToList<T>();
            }
            return Enumerable.Empty<T>().ToList();
        }
    }
}
