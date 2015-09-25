using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Model;
using DAO.Repository;

namespace BLL.Services
{
    /// <summary>
    /// Class for working with payments.
    /// </summary>
    public static class PaymentService
    {
        /// <summary>
        /// Method for prepare payment
        /// </summary>
        public static void PreparePayment(Operation operation, IRepositoryFactory factory, string userId)
        {
            operation.Type = OperationType.PreparedPayment;
            Payment(operation, factory, userId);
        }

        /// <summary>
        /// Method for create new payment.
        /// </summary>
        public static void PayPayment(Operation operation, IRepositoryFactory factory, string userId)
        {
            operation.Type = OperationType.Paymnet;
            Payment(operation, factory, userId);
        }

        /// <summary>
        /// Method for cancel prepared payment.
        /// </summary>
        /// <param name="id">id of payment</param>
        /// <param name="userId">id of user which has this payment</param>
        public static void CancelPreparedPayment(long id, string userId, IRepositoryFactory factory)
        {
            var operation = factory.OperationRepository.FindById(id);
            if(operation==null)
                throw new ValidationException("Нельзя отменить чужую операцию");
            var account = operation.Account;
            if(account.User.Id!=userId)
                throw new ValidationException("Нельзя отменить чужую операцию");
            if (operation.Type != OperationType.PreparedPayment)
                throw new ValidationException("Невозможно отменить выполненный платеж.");
            if (operation.Account.IsBlocked)
                throw new ValidationException("Ваша карта заблокирована.");
            account.Balance += operation.Amount;
            account.Operations.Remove(operation);
            factory.OperationRepository.Delete(operation.OperationId);
            factory.AccountRepository.Edit(account);
        }

        /// <summary>
        /// Method for replensish account.
        /// </summary>
        public static void ReplenishAccount(Operation operation, IRepositoryFactory factory, string userId)
        {
            var account = factory.AccountRepository.FindById(operation.AccountId);
            if(!account.UserId.Equals(userId) || account.IsBlocked)
                throw new ValidationException("Нельзя пополнить данный счет.");
            operation.Type = OperationType.Replenishment;
            account.Balance += operation.Amount;
            operation.OperationDate = DateTime.Now;
            account.Operations.Add(operation);
            factory.AccountRepository.Edit(account);
        }

        /// <summary>
        /// Method for accept prepared payment.
        /// </summary>
        /// <param name="operationId">id of payment</param>
        /// <param name="userId">id of user which has this payment</param>
        public static void AcceptPreparedPayment(long operationId, string userId, IRepositoryFactory factory)
        {
            var operation = factory.OperationRepository.FindById(operationId);
            if (operation.Account.User.Id != userId)
                throw new ValidationException("Нельзя подтвердить чужую операцию");
            if (operation.Type != OperationType.PreparedPayment)
                throw new ValidationException("Этот платеж невозможно подтвердить.");
            if (operation.Account.IsBlocked)
                throw new ValidationException("Ваша карта заблокирована.");
            operation.Type = OperationType.Paymnet;
            CheckCardOperation(factory, userId, operation);
            factory.OperationRepository.Edit(operation);
        }

        private static void CheckCardOperation(IRepositoryFactory factory, string userId, Operation operation)
        {
            if (!(operation is CardOperation)) return;
            var cOperation = operation as CardOperation;
            cOperation.CardNumber = new string(cOperation.CardNumber.Where(c => c != ' ').ToArray());
            var card = factory.CardRepository.Find(c => c.Number.Equals(cOperation.CardNumber)).FirstOrDefault();
            if (card != null)
            {
                var replenishmentOperation = new CardOperation
                {
                    Amount = operation.Amount,
                    CardNumber = cOperation.CardNumber,
                    AccountId = card.Account.AccountId
                };
                ReplenishAccount(replenishmentOperation, factory, userId);
            }
        }

        private static void Payment(Operation operation, IRepositoryFactory factory, string userId)
        {
            var account = factory.AccountRepository.FindById(operation.AccountId);
            if (!account.UserId.Equals(userId) || account.IsBlocked)
                throw new ValidationException("Нельзя использовать данный счет.");
            if (operation.Amount > account.Balance)
                throw new ValidationException("Сумма платежа больше, чем баланс на счету.");
            operation.OperationDate = DateTime.UtcNow;
            if(operation.Type!= OperationType.PreparedPayment)
                CheckCardOperation(factory, userId, operation);
            account.Balance -= operation.Amount;
            account.Operations.Add(operation);
            factory.AccountRepository.Edit(account);
        }
    }
}
