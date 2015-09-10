using DAO.Model;
using DAO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public static class OperationServices
    {
        public static object Operations(IRepositoryFactory factory, long accountId, string userId, bool needCheck)
        {
            var account = factory.AccountRepository.FindById(accountId);
            if (account == null || (needCheck && account.UserId != userId))
                return null;
            return account.Operations.Select(o => new { 
                OperationDate = o.OperationDate.ToShortDateString(),
                o.Amount,
                Type = o.Type.GetDescription(),
                Id = o.OperationId
            }).ToList();
        }
    }
}