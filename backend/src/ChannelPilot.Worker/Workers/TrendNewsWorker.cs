using ChannelPilot.Worker.Jobs;
using ChannelPilot.Worker.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ChannelPilot.Worker.Workers;

public sealed class TrendNewsWorker(
    CollectTrendNewsJob job,
    IOptions<TrendWorkerOptions> options,
    ILogger<TrendNewsWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Trend news worker started.");

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (options.Value.Enabled)
                {
                    try
                    {
                        await job.ExecuteAsync(stoppingToken);
                    }
                    catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                    {
                        break;
                    }
                    catch (Exception exception)
                    {
                        logger.LogError(exception, "Trend news job failed.");
                    }
                }

                try
                {
                    await Task.Delay(GetDelay(options.Value.IntervalSeconds), stoppingToken);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
            }
        }
        finally
        {
            logger.LogInformation("Trend news worker stopped.");
        }
    }

    private static TimeSpan GetDelay(int intervalSeconds) =>
        TimeSpan.FromSeconds(Math.Max(1, intervalSeconds));
}
