using PaymentApp.Application.Interfaces.Repositories;
using PaymentApp.Application.Interfaces.Services;
using PaymentApp.Domain.Enums;

namespace PaymentApp.Application.Services;

public class AutoConfirmService : IAutoConfirmService
{
    private readonly ITransactionRepository _txRepo;

    public AutoConfirmService(ITransactionRepository txRepo)
    {
        _txRepo = txRepo;
    }

    public async Task ConfirmExpiredTransactionsAsync()
    {
        var expiredTxs = await _txRepo.GetExpiredHeldTransactionsAsync();

        foreach (var tx in expiredTxs)
        {
            tx.Status = TransactionStatus.Confirmed;
            await _txRepo.UpdateAsync(tx);
        }
    }

}
