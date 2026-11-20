using ChannelPilot.Application.Abstractions.Sources;
using ChannelPilot.Application.DTOs.Content;
using ChannelPilot.Domain.Content;

namespace ChannelPilot.Infrastructure.Sources;

public sealed class FakeContentSourceReader : IContentSourceReader
{
    public Task<IReadOnlyList<CollectedContentDto>> ReadAsync(
        ContentSource source,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult<IReadOnlyList<CollectedContentDto>>([]);
    }
}
