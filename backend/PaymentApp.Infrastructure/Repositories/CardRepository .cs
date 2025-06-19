using Microsoft.EntityFrameworkCore;
using PaymentApp.Application.Interfaces.Repositories;
using PaymentApp.Domain.Entities;
using PaymentApp.Infrastructure.Data;

namespace PaymentApp.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    private readonly AppDbContext _context;

    public CardRepository(AppDbContext context) => _context = context;

    public async Task<Card?> GetByNumberAsync(string cardNumber) =>
        await _context.Cards.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);

    public async Task<bool> ExistsAsync(string cardNumber) =>
        await _context.Cards.AnyAsync(c => c.CardNumber == cardNumber);

    public async Task UpdateAsync(Card card)
    {
        _context.Cards.Update(card);
        await _context.SaveChangesAsync();
    }
}
