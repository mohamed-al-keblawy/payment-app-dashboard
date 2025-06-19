using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Interfaces.Repositories;

public interface ICardRepository
{
    Task<Card?> GetByNumberAsync(string cardNumber);
    Task<bool> ExistsAsync(string cardNumber);
    Task UpdateAsync(Card card);

    Task AddBalanceAsync(string cardNumber, decimal amount);
}