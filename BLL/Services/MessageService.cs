using DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DAO.Repository;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BLL.Services
{
    /// <summary>
    /// Class for working with messages.
    /// </summary>
    public static class MessageService
    {
        /// <summary>
        /// Send new message to random support user.
        /// </summary>
        public static void SendToSupport(IRepositoryFactory factory, Message message)
        {
            var supports = factory.GetUserRepository(null).Get("Support");
            var rnd = new Random();
            var supportUser = supports[rnd.Next(supports.Count + 1)];
            message.To = supportUser;
            SendMessage(factory, message);
        }

        /// <summary>
        /// Send answer message.
        /// </summary>
        public static void Answer(IRepositoryFactory factory, Message message)
        {
            var oldId = message.MessageId;
            message.MessageId = 0;
            var user = factory.MessageRepository.FindById(oldId).From;
            message.To = user;
            SendMessage(factory, message);
        }

        private static void SendMessage(IRepositoryFactory factory, Message message)
        {
            message.Date = DateTime.Now;
            message.From = factory.GetUserRepository(null).FindById(message.FromId);
            factory.MessageRepository.Add(message);
        }
        /// <summary>
        /// Method for get selected message.
        /// </summary>
        public static Message GetMessage(IRepositoryFactory factory, long id)
        {
            return factory.MessageRepository.FindById(id);
        }

        /// <summary>
        /// Method for get inbox messages of current message.
        /// </summary>
        public static object GetInbox(IRepositoryFactory factory, string userId)
        {
            return factory.MessageRepository
                .Find(m => m.To.Id == userId)
                .OrderByDescending(m=>m.Date)
                .Select(m=>new
                {
                    From = m.From.LastName + " " + m.From.FirstName,
                    m.Theme,
                    Id = m.MessageId,
                    Date = m.Date.ToString("dd.MM.yyyy HH:mm")
                }).ToList();
        }

        /// <summary>
        /// Method for get inbox messages of current message.
        /// </summary>
        public static object GetOutbox(IRepositoryFactory factory, string userId)
        {
            return factory.MessageRepository
                .Find(m => m.From.Id == userId)
                .OrderByDescending(m => m.Date)
                .Select(m => new
                {
                    To = m.To.LastName + " " + m.To.FirstName,
                    m.Theme,
                    Id = m.MessageId,
                    Date = m.Date.ToString("dd.MM.yyyy HH:mm")
                }).ToList();
        }
    }
}
