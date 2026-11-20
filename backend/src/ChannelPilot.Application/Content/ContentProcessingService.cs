using ChannelPilot.Application.DTOs.Content;
using ChannelPilot.Domain.Content;

namespace ChannelPilot.Application.Content;

public sealed class ContentProcessingService
{
    public ContentItem CreateCollectedItem(CollectedContentDto collected, ContentType type)
    {
        return new ContentItem
        {
            Type = type,
            Status = ContentStatus.Collected,
            OriginalTitle = collected.Title,
            OriginalSummary = collected.Summary,
            OriginalBody = collected.Body,
            SourceUrl = collected.SourceUrl,
            ImageUrl = collected.ImageUrl,
            SourceName = collected.SourceName,
            CollectedAt = collected.CollectedAt,
            CreatedAt = collected.CollectedAt
        };
    }
}
