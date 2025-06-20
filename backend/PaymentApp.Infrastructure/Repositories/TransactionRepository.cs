using Microsoft.EntityFrameworkCore;
using PaymentApp.Application.DTOs.Requests;
using PaymentApp.Application.DTOs.Responses;
using PaymentApp.Application.Interfaces.Repositories;
using PaymentApp.Domain.Entities;
using PaymentApp.Domain.Enums;
using PaymentApp.Infrastructure.Data;

namespace PaymentApp.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context) => _context = context;

    public async Task AddAsync(PaymentTransaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task<PaymentTransaction?> GetByTransactionIdAsync(string transactionId) =>
    await _context.Transactions.FirstOrDefaultAsync(t => t.TransactionId == transactionId);

    public async Task UpdateAsync(PaymentTransaction tx)
    {
        _context.Transactions.Update(tx);
        await _context.SaveChangesAsync();
    }

    public async Task<List<PaymentTransaction>> GetExpiredHeldTransactionsAsync()
    {
        return await _context.Transactions
            .Where(t => t.Status == TransactionStatus.Held && t.RefundCodeExpiry < DateTime.UtcNow)
            .ToListAsync();
    }

    public async Task<PagedResult<PaymentReportItem>> GetPaymentsReportAsync(PaymentsReportFilter filter)
    {
        var query = _context.Transactions.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.CardNumber))
            query = query.Where(t => t.CardNumber == filter.CardNumber);

        if (filter.Status.HasValue)
            query = query.Where(t => t.Status == filter.Status);

        if (filter.FromDate.HasValue)
            query = query.Where(t => t.RequestedAt >= filter.FromDate);

        if (filter.ToDate.HasValue)
            query = query.Where(t => t.RequestedAt <= filter.ToDate);

        var total = await query.CountAsync();

        var result = await query
            .OrderByDescending(t => t.RequestedAt)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(t => new PaymentReportItem
            {
                TransactionId = t.TransactionId,
                CardNumber = t.CardNumber,
                Amount = t.Amount,
                RequestedAt = t.RequestedAt,
                Status = t.Status
            })
            .ToListAsync();

        return new PagedResult<PaymentReportItem>
        {
            Items = result,
            TotalCount = total
        };
    }

}
