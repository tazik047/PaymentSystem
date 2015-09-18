using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using DAO.Repository;
using DAO.Model;

namespace EntityFrameworkDAO.Repository
{
    class MessageRepository : IMessageRepository
    {
        private readonly PaymentDbContext _db;

        public MessageRepository(PaymentDbContext context)
        {
            _db = context;
        }

        public void Add(Message item)
        {
            _db.Messages.Add(item);
            _db.SaveChanges();
        }

        public void Edit(Message item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(long id)
        {
            var item = FindById(id);
            if (item == null) return;
            _db.Messages.Remove(item);
            _db.SaveChanges();
        }

        public List<Message> Get()
        {
            return _db.Messages.ToList();
        }

        public List<Message> Get(int skip, int take)
        {
            return Get().Skip(skip).Take(take).ToList();
        }

        public Message FindById(long id)
        {
            return _db.Messages.Find(id);
        }

        public List<Message> Find(Func<Message, bool> predicate)
        {
            return Get().Where(predicate).ToList();
        }
    }
}
