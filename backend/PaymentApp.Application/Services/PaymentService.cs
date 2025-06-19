using PaymentApp.Application.DTOs;
using PaymentApp.Application.Interfaces.Repositories;
using PaymentApp.Application.Interfaces.Services;
using PaymentApp.Domain.Entities;
using PaymentApp.Domain.Enums;

namespace PaymentApp.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly ICardRepository _cardRepo;
    private readonly ITransactionRepository _txRepo;

    public PaymentService(ICardRepository cardRepo, ITransactionRepository txRepo)
    {
        _cardRepo = cardRepo;
        _txRepo = txRepo;
    }

    public async Task<PaymentResponse> ProcessPaymentAsync(PaymentRequest request)
    {
        var card = await _cardRepo.GetByNumberAsync(request.CardNumber);
        if (card == null || card.Balance < request.Amount)
            throw new InvalidOperationException("Insufficient funds or card not found.");

        // Generate Transaction ID (10-20 chars)
        string transactionId = Guid.NewGuid().ToString("N")[..16];

        // Generate 4-digit Refund Code
        string refundCode = new Random().Next(1000, 9999).ToString();

        var tx = new PaymentTransaction
        {
            Id = Guid.NewGuid(),
            TransactionId = transactionId,
            CardNumber = card.CardNumber,
            Amount = request.Amount,
            RefundCode = refundCode,
            RefundCodeExpiry = DateTime.Today.AddDays(1), // Valid until 12:00 AM next day
            IsConfirmed = false
        };

        await _txRepo.AddAsync(tx);

        return new PaymentResponse
        {
            TransactionId = transactionId,
            RefundCode = refundCode
        };
    }


    public async Task<bool> RefundAsync(RefundRequest request)
    {
        var tx = await _txRepo.GetByTransactionIdAsync(request.TransactionId);
        if (tx == null)
            throw new InvalidOperationException("Transaction not found.");

        if (tx.Status != TransactionStatus.Held)
            throw new InvalidOperationException("Transaction already refunded or confirmed.");

        if (tx.RefundCode != request.RefundCode)
            throw new InvalidOperationException("Invalid refund code.");

        if (DateTime.UtcNow > tx.RefundCodeExpiry)
            throw new InvalidOperationException("Refund code expired.");

        // Refund logic
        await _cardRepo.AddBalanceAsync(tx.CardNumber, tx.Amount);
        tx.Status = TransactionStatus.Refunded;
        await _txRepo.UpdateAsync(tx);

        return true;
    }

}
