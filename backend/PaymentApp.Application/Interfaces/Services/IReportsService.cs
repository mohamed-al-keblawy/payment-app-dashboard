using PaymentApp.Application.DTOs.Requests;
using PaymentApp.Application.DTOs.Responses;

namespace PaymentApp.Application.Interfaces.Services;

public interface IReportsService
{
    Task<PagedResult<PaymentReportItem>> GetPaymentsReportAsync(PaymentsReportFilter filter);
}
