using System;
using System.Collections.Generic;
using System.Linq;
using DAO.Model;
using DAO.Repository;

namespace BLL.Services
{
    /// <summary>
    /// Class for working with requests of users.
    /// </summary>
    public static class RequestService
    {
        /// <summary>
        /// Add new request for unblocking account.
        /// </summary>
        /// <param name="id">id of account to unblock</param>
        /// <param name="userId">id of current user</param>
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

        /// <summary>
        /// Accept request and unblock account
        /// </summary>
        /// <param name="id">id of request</param>
        public static void AcceptRequest(long id, IRepositoryFactory factory)
        {
            var request = factory.RequestRepository.FindById(id);
            if (request == null) return;
            factory.RequestRepository.Delete(id);
        }

        /// <summary>
        /// Get number of all requests.
        /// </summary>
        public static long CountRequest(IRepositoryFactory factory)
        {
            return factory.RequestRepository.Get().LongCount();
        }

        /// <summary>
        /// Take first N request.
        /// </summary>
        /// <param name="n">number of requests</param>
        public static List<Request> FirstNRequests(IRepositoryFactory factory, int n)
        {
            return factory.RequestRepository.Get()
                .OrderByDescending(r => r.Date)
                .Take(n)
                .ToList();
        } 

        /// <summary>
        /// Get all request.
        /// </summary>
        public static object Requests(IRepositoryFactory factory)
        {
            return factory.RequestRepository.Get().OrderByDescending(r => r.Date).Select(r => new
            {
                Id = r.RequestId,
                Date = r.Date.ToString("dd.MM.yyyy HH:mm"),
                r.Account.Card.Name
            }).ToList();
        }
    }
}
