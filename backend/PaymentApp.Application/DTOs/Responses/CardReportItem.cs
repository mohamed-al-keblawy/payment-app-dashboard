namespace PaymentApp.Application.DTOs.Responses;

public class CardReportItem
{
    public string CardNumber { get; set; } = string.Empty;
    public string CardHolder { get; set; } = string.Empty;
    public decimal Balance { get; set; }
}
