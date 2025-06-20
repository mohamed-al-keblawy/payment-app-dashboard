// API/Controllers/CardController.cs
using Microsoft.AspNetCore.Mvc;
using PaymentApp.Application.DTOs;
using PaymentApp.Application.DTOs.Requests;
using PaymentApp.Application.Interfaces.Services;
using PaymentApp.Application.Services;

namespace PaymentApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardController : ControllerBase
{
    private readonly ICardService _cardService;
    private readonly IReportsService _reportsService;

    public CardController(ICardService cardService, IReportsService reportsService)
    {
        _cardService = cardService;
        _reportsService = reportsService;
    }

    [HttpPost("validate")]
    public async Task<IActionResult> ValidateCard([FromBody] CardValidationRequest request)
    {
        var isValid = await _cardService.ValidateCardAsync(request.CardNumber, request.CardHolder, request.ExpiryDate);
        return Ok(new { Valid = isValid });
    }

    [HttpGet("cards")]
    public async Task<IActionResult> GetCardReport([FromQuery] CardReportFilter filter)
    {
        var result = await _reportsService.GetCardReportAsync(filter);
        return Ok(result);
    }

}
