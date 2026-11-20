using ChannelPilot.Domain.Common;

namespace ChannelPilot.Domain.Publishing;

public sealed class PublishedPost : Entity
{
    public long TelegramChannelId { get; set; }

    public long? PublishQueueItemId { get; set; }

    public long? TelegramMessageId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Caption { get; set; } = string.Empty;

    public DateTime PublishedAt { get; set; }

    public string? SourceUrl { get; set; }
}
