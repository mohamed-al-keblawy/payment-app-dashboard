using Microsoft.AspNetCore.Mvc;
using PaymentApp.Application.DTOs;
using PaymentApp.Application.Interfaces.Services;

namespace PaymentApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("process")]
    public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
    {
        var response = await _paymentService.ProcessPaymentAsync(request);
        return Ok(response);
    }

    [HttpPost("refund")]
    public async Task<IActionResult> Refund([FromBody] RefundRequest request)
    {
        var result = await _paymentService.RefundAsync(request);
        return Ok(new { Refunded = result });
    }

}
