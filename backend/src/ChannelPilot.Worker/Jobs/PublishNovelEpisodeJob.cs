using Microsoft.Extensions.Logging;

namespace ChannelPilot.Worker.Jobs;

public sealed class PublishNovelEpisodeJob(ILogger<PublishNovelEpisodeJob> logger)
{
    public Task ExecuteAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        logger.LogInformation("Novel publishing placeholder ran.");
        return Task.CompletedTask;
    }
}
