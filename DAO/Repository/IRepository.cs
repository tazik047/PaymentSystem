using System;
using System.Collections.Generic;

namespace DAO.Repository
{
    public interface IRepository<T> where T : class
    {
        void Add(T item);
        void Edit(T item);
        void Delete(long id);
        
        List<T> GetAll();
        List<T> GetFirtNItem(int n);
        T FindById(long id);

        List<T> Find(Func<T, bool> predicate);
    }
}
