using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Model;
using DAO.Repository;

namespace BLL.Services
{
    public static class PaymentService
    {
        public static void PreparePayment(Operation operation, IRepositoryFactory factory, string userId)
        {
            operation.Type = OperationType.PreparedPayment;
            Payment(operation, factory, userId);
        }

        public static void PayPayment(Operation operation, IRepositoryFactory factory, string userId)
        {
            operation.Type = OperationType.Paymnet;
            Payment(operation, factory, userId);
        }

        public static void CancelPreparedPayment(Account account, Operation operation, IRepositoryFactory factory)
        {
            if (operation.Type != OperationType.PreparedPayment)
                throw new ValidationException("Невозможно отменить выполненный платеж.");
            account.Balance += operation.Amount;
            account.Operations.Remove(operation);
            factory.OperationRepository.Delete(operation.OperationId);
            factory.AccountRepository.Edit(account);
        }

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

        public static void AcceptPreparedPayment(long operationId, IRepositoryFactory factory, string userId)
        {
            var operation = factory.OperationRepository.FindById(operationId);
            if (operation.Type != OperationType.PreparedPayment)
                throw new ValidationException("Этот платеж невозможно подтвердить.");
            operation.Type = OperationType.Paymnet;
            factory.OperationRepository.Edit(operation);
            if (operation is CardOperation)
            {
                var cOperation = operation as CardOperation;
                var card = factory.CardRepository.Find(c => c.Number.Equals(cOperation.CardNumber)).FirstOrDefault();
                if (card != null)
                {
                    var replenishmentOperation = new CardOperation
                    {
                        Amount = operation.Amount,
                        CardNumber = cOperation.CardNumber,
                        AccountId = card.AccountId
                    };
                    ReplenishAccount(replenishmentOperation, factory, userId);
                }
            }
        }

        private static void Payment(Operation operation, IRepositoryFactory factory, string userId)
        {
            var account = factory.AccountRepository.FindById(operation.AccountId);
            if (!account.UserId.Equals(userId) || account.IsBlocked)
                throw new ValidationException("Нельзя использовать данный счет.");
            if (operation.Amount > account.Balance)
                throw new ValidationException("Сумма платежа больше, чем баланс на счету.");
            operation.OperationDate = DateTime.Now;
            account.Balance -= operation.Amount;
            account.Operations.Add(operation);
            factory.AccountRepository.Edit(account);
        }
    }
}
