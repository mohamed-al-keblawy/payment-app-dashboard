using Microsoft.EntityFrameworkCore;
using PaymentApp.Application.DTOs.Requests;
using PaymentApp.Application.DTOs.Responses;
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

    public async Task AddBalanceAsync(string cardNumber, decimal amount)
    {
        var card = await GetByNumberAsync(cardNumber);
        if (card != null)
        {
            card.Balance += amount;
            _context.Cards.Update(card);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<PagedResult<CardReportItem>> GetCardReportAsync(CardReportFilter filter)
    {
        var query = _context.Cards.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.CardNumber))
            query = query.Where(c => c.CardNumber.Contains(filter.CardNumber));

        if (!string.IsNullOrWhiteSpace(filter.CardHolder))
            query = query.Where(c => c.CardHolder.Contains(filter.CardHolder));

        var total = await query.CountAsync();

        var result = await query
            .OrderByDescending(c => c.Balance)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(c => new CardReportItem
            {
                CardNumber = c.CardNumber,
                CardHolder = c.CardHolder,
                Balance = c.Balance
            })
            .ToListAsync();

        return new PagedResult<CardReportItem>
        {
            Items = result,
            TotalCount = total
        };
    }

}
