using System.Collections.Generic;
using DAO.Model;

namespace DAO.Repository
{
    public interface IOperationRepository : IRepository<Operation>
    {
        List<T> Get<T>() where T : Operation;
    }
}
