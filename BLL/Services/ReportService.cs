using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Repository;
using DAO.Model;

namespace BLL.Services
{
    public static class ReportService
    {
        public static List<Tuple<User, String[], decimal, decimal>> UserOperations(IRepositoryFactory factory)
        {
            var users = factory.GetUserRepository(null).Get();
            var result = new List<Tuple<User, String[], decimal, decimal>>();
            var now = DateTime.Now.Month;
            foreach (var u in users)
            {
                var accounts = u.Accounts.Where(a => a.Operations.Any(o => o.OperationDate.Month == now));
                var allOperations = accounts.SelectMany(a => a.Operations).Where(o=>o.OperationDate.Month == now);
                var payments = allOperations.Where(o => o.Type == OperationType.Paymnet);
                var replenishments = allOperations.Where(o => o.Type == OperationType.Replenishment);
                if (payments.Sum(o => o.Amount) > 50000 || replenishments.Sum(o => o.Amount) > 50000)
                    result.Add(new Tuple<User, String[], decimal, decimal>(u, 
                        accounts.Select(a=>a.Card.Name).ToArray(), 
                        payments.Sum(o => o.Amount), 
                        replenishments.Sum(o => o.Amount)));
            }
            return result;
        }
    }
}
