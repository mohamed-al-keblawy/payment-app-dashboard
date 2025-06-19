// API/Controllers/CardController.cs
using Microsoft.AspNetCore.Mvc;
using PaymentApp.Application.DTOs;
using PaymentApp.Application.Interfaces.Services;

namespace PaymentApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardController : ControllerBase
{
    private readonly ICardService _cardService;

    public CardController(ICardService cardService)
    {
        _cardService = cardService;
    }

    [HttpPost("validate")]
    public async Task<IActionResult> ValidateCard([FromBody] CardValidationRequest request)
    {
        var isValid = await _cardService.ValidateCardAsync(request.CardNumber, request.CardHolder, request.ExpiryDate);
        return Ok(new { Valid = isValid });
    }
}
