using PaymentApp.Domain.Enums;

namespace PaymentApp.Application.DTOs.Responses;

public class PaymentReportItem
{
    public string TransactionId { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime RequestedAt { get; set; }
    public TransactionStatus Status { get; set; }
}
