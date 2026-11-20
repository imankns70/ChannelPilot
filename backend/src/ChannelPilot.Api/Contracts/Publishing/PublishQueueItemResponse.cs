using ChannelPilot.Domain.Publishing;

namespace ChannelPilot.Api.Contracts.Publishing;

public sealed record PublishQueueItemResponse(
    long Id,
    long TelegramChannelId,
    long? ContentItemId,
    long? NovelEpisodeId,
    string Title,
    string Caption,
    string? ImageUrl,
    PublishStatus Status,
    DateTime? ScheduledFor,
    bool RequiresApproval,
    DateTime? ApprovedAt,
    DateTime? PublishedAt,
    PublishFailureReason? FailureReason,
    int RetryCount);
