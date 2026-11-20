using ChannelPilot.Application.Abstractions.Persistence;

namespace ChannelPilot.Application.Content;

public sealed class ContentDeduplicationService(IContentRepository contentRepository)
{
    public Task<bool> IsDuplicateAsync(
        string contentHash,
        CancellationToken cancellationToken = default)
    {
        return contentRepository.ExistsByHashAsync(contentHash, cancellationToken);
    }
}
