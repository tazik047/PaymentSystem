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
        public static void PreparePayment(Account account, Operation operation, IRepositoryFactory factory)
        {
            operation.Type = OperationType.PreparedPayment;
            Payment(account, operation, factory);
        }

        public static void PayPayment(Account account, Operation operation, IRepositoryFactory factory)
        {
            operation.Type = OperationType.Paymnet;
            Payment(account, operation, factory);
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

        public static void ReplenishAccount(Account account, Operation operation, IRepositoryFactory factory)
        {
            operation.Type = OperationType.Replenishment;
            account.Balance += operation.Amount;
            operation.OperationDate = DateTime.Now;
            account.Operations.Add(operation);
            factory.AccountRepository.Edit(account);
        }

        public static void AcceptPreparedPayment(long operationId, IRepositoryFactory factory)
        {
            var operation = factory.OperationRepository.FindById(operationId);
            if(operation.Type!=OperationType.PreparedPayment)
                throw new ValidationException("Этот платеж невозможно подтвердить.");
            operation.Type = OperationType.Paymnet;
            factory.OperationRepository.Edit(operation);
        }

        private static void Payment(Account account, Operation operation, IRepositoryFactory factory)
        {
            if (operation.Amount > account.Balance)
                throw new ValidationException("Сумма платежа больше, чем баланс на счету.");
            operation.OperationDate = DateTime.Now;
            account.Balance -= operation.Amount;
            account.Operations.Add(operation);
            factory.AccountRepository.Edit(account);
        }


    }
}
