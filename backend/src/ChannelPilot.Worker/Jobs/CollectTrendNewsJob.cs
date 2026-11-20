using Microsoft.Extensions.Logging;

namespace ChannelPilot.Worker.Jobs;

public sealed class CollectTrendNewsJob(ILogger<CollectTrendNewsJob> logger)
{
    public Task ExecuteAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        logger.LogInformation("Trend collection placeholder ran.");
        return Task.CompletedTask;
    }
}
