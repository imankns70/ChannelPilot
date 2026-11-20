using ChannelPilot.Worker.Jobs;
using ChannelPilot.Worker.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ChannelPilot.Worker.Workers;

public sealed class PublishQueueWorker(
    PublishQueuedPostsJob job,
    IOptions<PublishQueueWorkerOptions> options,
    ILogger<PublishQueueWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Publish queue worker started.");

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
                        logger.LogError(exception, "Publish queue job failed.");
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
            logger.LogInformation("Publish queue worker stopped.");
        }
    }

    private static TimeSpan GetDelay(int intervalSeconds) =>
        TimeSpan.FromSeconds(Math.Max(1, intervalSeconds));
}
