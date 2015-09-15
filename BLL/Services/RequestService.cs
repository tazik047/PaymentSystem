using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Model;
using DAO.Repository;

namespace BLL.Services
{
    public static class RequestService
    {
        public static void AddRequest(long id, string userId, IRepositoryFactory factory)
        {
            var account = factory.AccountRepository.FindById(id);
            if (account == null)
                throw new ValidationException("Нельзя разблокировать чужой счет");
            if (account.User.Id != userId)
                throw new ValidationException("Нельзя разблокировать чужой счет");
            if(!account.IsBlocked)
                throw new ValidationException("Аккаунт не заблокирован.");
            var oldRequest = factory.RequestRepository.Find(r => r.Account.AccountId == id).FirstOrDefault();
            if (oldRequest == null)
            {
                var request = new Request
                {
                    Account = account,
                    Date = DateTime.Now
                };
                factory.RequestRepository.Add(request);
            }
            else
            {
                oldRequest.Date = DateTime.Now;
                factory.RequestRepository.Edit(oldRequest);
            }
        }

        public static void AcceptRequest(long id, IRepositoryFactory factory)
        {
            var request = factory.RequestRepository.FindById(id);
            if (request == null) return;
            //AccountService.UnBlockAccount(factory, request.Account.AccountId, false);
            factory.RequestRepository.Delete(id);
        }

        public static long CountRequest(IRepositoryFactory factory)
        {
            return factory.RequestRepository.Get().LongCount();
        }

        public static List<Request> FirstNRequests(IRepositoryFactory factory, int n)
        {
            return factory.RequestRepository.Get()
                .OrderByDescending(r => r.Date)
                .Take(n)
                .ToList();
        } 

        public static object Requests(IRepositoryFactory factory)
        {
            return factory.RequestRepository.Get().OrderByDescending(r => r.Date).Select(r => new
            {
                Id = r.RequestId,
                r.Date,
                r.Account.Card.Name
            }).ToList();
        }
    }
}
