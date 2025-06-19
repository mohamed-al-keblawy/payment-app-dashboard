using PaymentApp.Domain.Enums;

namespace PaymentApp.Domain.Entities;

public class PaymentTransaction
{
    public Guid Id { get; set; }
    public string TransactionId { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    public bool IsConfirmed { get; set; } = false;
    public string RefundCode { get; set; } = string.Empty;
    public DateTime RefundCodeExpiry { get; set; }

    public TransactionStatus Status { get; set; } = TransactionStatus.Held;

}
