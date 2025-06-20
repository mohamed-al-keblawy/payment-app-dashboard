using PaymentApp.Application.DTOs.Requests;
using PaymentApp.Application.DTOs.Responses;
using PaymentApp.Application.Interfaces.Repositories;
using PaymentApp.Application.Interfaces.Services;

namespace PaymentApp.Application.Services;

public class ReportsService : IReportsService
{
    private readonly ITransactionRepository _txRepo;
    private readonly ICardRepository _cardRepo;

    public ReportsService(ITransactionRepository txRepo, ICardRepository cardRepo)
    {
        _txRepo = txRepo;
        _cardRepo = cardRepo;
    }

    public Task<PagedResult<PaymentReportItem>> GetPaymentsReportAsync(PaymentsReportFilter filter)
    {
        return _txRepo.GetPaymentsReportAsync(filter);
    }

    public async Task<PagedResult<CardReportItem>> GetCardReportAsync(CardReportFilter filter)
    {
        return await _cardRepo.GetCardReportAsync(filter);
    }

}
