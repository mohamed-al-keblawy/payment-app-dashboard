using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PaymentApp.Application.Interfaces.Services;

namespace PaymentApp.Infrastructure.Workers;

public class AutoConfirmWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<AutoConfirmWorker> _logger;

    public AutoConfirmWorker(IServiceScopeFactory scopeFactory, ILogger<AutoConfirmWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var targetTime = DateTime.Today.AddDays(1); // next 12:00 AM

            var delay = targetTime - now;
            _logger.LogInformation($"[AutoConfirmWorker] Sleeping for {delay.TotalMinutes} minutes until {targetTime}");

            await Task.Delay(delay, stoppingToken);

            using var scope = _scopeFactory.CreateScope();
            var confirmService = scope.ServiceProvider.GetRequiredService<IAutoConfirmService>();
            await confirmService.ConfirmExpiredTransactionsAsync();

            _logger.LogInformation("[AutoConfirmWorker] Auto-confirmation run completed.");
        }
    }

    // For Testing runs every 1 minute
    //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //{
    //    while (!stoppingToken.IsCancellationRequested)
    //    {
    //        using var scope = _scopeFactory.CreateScope();
    //        var confirmService = scope.ServiceProvider.GetRequiredService<IAutoConfirmService>();
    //        await confirmService.ConfirmExpiredTransactionsAsync();

    //        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
    //    }
    //}

}
