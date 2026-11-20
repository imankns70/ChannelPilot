using Microsoft.Extensions.Logging;

namespace ChannelPilot.Worker.Jobs;

public sealed class PublishQueuedPostsJob(ILogger<PublishQueuedPostsJob> logger)
{
    public Task ExecuteAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        logger.LogInformation("Publish queue placeholder ran.");
        return Task.CompletedTask;
    }
}
