using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task AddAsync(PaymentTransaction transaction);
    Task<PaymentTransaction?> GetByTransactionIdAsync(string transactionId);
    Task UpdateAsync(PaymentTransaction tx);
}
