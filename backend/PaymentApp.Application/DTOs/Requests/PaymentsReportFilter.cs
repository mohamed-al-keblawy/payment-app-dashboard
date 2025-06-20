using PaymentApp.Domain.Enums;

namespace PaymentApp.Application.DTOs.Requests;

public class PaymentsReportFilter
{
    public string? CardNumber { get; set; }
    public TransactionStatus? Status { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
