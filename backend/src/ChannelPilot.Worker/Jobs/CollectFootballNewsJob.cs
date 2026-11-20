using Microsoft.Extensions.Logging;

namespace ChannelPilot.Worker.Jobs;

public sealed class CollectFootballNewsJob(ILogger<CollectFootballNewsJob> logger)
{
    public Task ExecuteAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        logger.LogInformation("Football collection placeholder ran.");
        return Task.CompletedTask;
    }
}
