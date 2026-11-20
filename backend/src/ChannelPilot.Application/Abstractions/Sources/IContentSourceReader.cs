using ChannelPilot.Application.DTOs.Content;
using ChannelPilot.Domain.Content;

namespace ChannelPilot.Application.Abstractions.Sources;

public interface IContentSourceReader
{
    Task<IReadOnlyList<CollectedContentDto>> ReadAsync(
        ContentSource source,
        CancellationToken cancellationToken = default);
}
