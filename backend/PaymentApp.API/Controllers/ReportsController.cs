using Microsoft.AspNetCore.Mvc;
using PaymentApp.Application.DTOs.Requests;
using PaymentApp.Application.Interfaces.Services;

namespace PaymentApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportsService _reportsService;

    public ReportsController(IReportsService reportsService)
    {
        _reportsService = reportsService;
    }

    [HttpGet("payments")]
    public async Task<IActionResult> GetPaymentsReport([FromQuery] PaymentsReportFilter filter)
    {
        var result = await _reportsService.GetPaymentsReportAsync(filter);
        return Ok(result);
    }
}
