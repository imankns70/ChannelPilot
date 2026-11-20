using ChannelPilot.Worker.Jobs;
using ChannelPilot.Worker.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ChannelPilot.Worker.Workers;

public sealed class NovelPublishingWorker(
    PublishNovelEpisodeJob job,
    IOptions<NovelWorkerOptions> options,
    ILogger<NovelPublishingWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Novel publishing worker started.");

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
                        logger.LogError(exception, "Novel publishing job failed.");
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
            logger.LogInformation("Novel publishing worker stopped.");
        }
    }

    private static TimeSpan GetDelay(int intervalSeconds) =>
        TimeSpan.FromSeconds(Math.Max(1, intervalSeconds));
}
