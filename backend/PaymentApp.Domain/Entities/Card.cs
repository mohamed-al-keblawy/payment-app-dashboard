namespace PaymentApp.Domain.Entities;

public class Card
{
    public Guid Id { get; set; }
    public string CardNumber { get; set; } = string.Empty;
    public string CardHolder { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
    public decimal Balance { get; set; }
}
