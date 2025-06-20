using PaymentApp.Application.DTOs.Requests;
using PaymentApp.Application.DTOs.Responses;
using PaymentApp.Application.Interfaces.Repositories;
using PaymentApp.Application.Interfaces.Services;

namespace PaymentApp.Application.Services;

public class ReportsService : IReportsService
{
    private readonly ITransactionRepository _txRepo;

    public ReportsService(ITransactionRepository txRepo)
    {
        _txRepo = txRepo;
    }

    public Task<PagedResult<PaymentReportItem>> GetPaymentsReportAsync(PaymentsReportFilter filter)
    {
        return _txRepo.GetPaymentsReportAsync(filter);
    }
}
