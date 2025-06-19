using Microsoft.EntityFrameworkCore;
using PaymentApp.Application.Interfaces.Repositories;
using PaymentApp.Domain.Entities;
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
    
}
