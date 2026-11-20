using ChannelPilot.Application.DTOs.Content;
using ChannelPilot.Domain.Content;

namespace ChannelPilot.Application.Abstractions.Ai;

public interface IAiContentRewriter
{
    Task<ContentRewriteDto> RewriteAsync(
        ContentItem content,
        string channelTone,
        CancellationToken cancellationToken = default);
}
