using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using DAO.Repository;
using DAO.Model;

namespace EntityFrameworkDAO.Repository
{
    class RequestRepository : IRequestRepository
    {
        private readonly PaymentDbContext _db;

        public RequestRepository(PaymentDbContext context)
        {
            _db = context;
        }

        public void Add(Request item)
        {
            _db.Requests.Add(item);
            _db.SaveChanges();
        }

        public void Edit(Request item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(long id)
        {
            var item = FindById(id);
            if (item == null) return;
            _db.Requests.Remove(item);
            _db.SaveChanges();
        }

        public List<Request> Get()
        {
            return _db.Requests.ToList();
        }

        public List<Request> Get(int skip, int take)
        {
            return Get().Skip(skip).Take(take).ToList();
        }

        public Request FindById(long id)
        {
            return _db.Requests.Find(id);
        }

        public List<Request> Find(Func<Request, bool> predicate)
        {
            return Get().Where(predicate).ToList();
        }
    }
}
