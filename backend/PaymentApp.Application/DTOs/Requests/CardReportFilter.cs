namespace PaymentApp.Application.DTOs.Requests;

public class CardReportFilter
{
    public string? CardNumber { get; set; }
    public string? CardHolder { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
