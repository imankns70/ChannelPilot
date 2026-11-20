using ChannelPilot.Application.Abstractions.Ai;
using ChannelPilot.Application.DTOs.Content;
using ChannelPilot.Domain.Content;

namespace ChannelPilot.Infrastructure.AI;

public sealed class FakeAiContentRewriter : IAiContentRewriter
{
    public Task<ContentRewriteDto> RewriteAsync(
        ContentItem content,
        string channelTone,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var caption = content.OriginalSummary ?? content.OriginalBody ?? content.OriginalTitle;
        return Task.FromResult(new ContentRewriteDto(
            content.OriginalTitle,
            caption,
            content.ImageUrl));
    }
}
