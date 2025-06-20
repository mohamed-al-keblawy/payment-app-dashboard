using PaymentApp.Application.DTOs;
using PaymentApp.Application.DTOs.Requests;

namespace PaymentApp.Application.Interfaces.Services;

public interface IPaymentService
{
    Task<PaymentResponse> ProcessPaymentAsync(PaymentRequest request);

    Task<bool> RefundAsync(RefundRequest request);

}
