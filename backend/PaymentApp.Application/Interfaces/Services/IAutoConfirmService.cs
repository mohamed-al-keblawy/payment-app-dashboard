namespace PaymentApp.Application.Interfaces.Services;

public interface IAutoConfirmService
{
    Task ConfirmExpiredTransactionsAsync();
}
