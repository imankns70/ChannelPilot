using ChannelPilot.Domain.Common;

namespace ChannelPilot.Domain.Publishing;

public sealed class PublishQueueItem : AuditableEntity
{
    public long TelegramChannelId { get; set; }

    public long? ContentItemId { get; set; }

    public long? NovelEpisodeId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Caption { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

    public PublishStatus Status { get; set; }

    public DateTime? ScheduledFor { get; set; }

    public bool RequiresApproval { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public DateTime? PublishedAt { get; set; }

    public PublishFailureReason? FailureReason { get; set; }

    public int RetryCount { get; set; }
}
