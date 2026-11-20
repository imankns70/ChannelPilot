using ChannelPilot.Domain.Publishing;

namespace ChannelPilot.Application.Publishing;

public sealed class PublishQueueService
{
    public PublishQueueItem Create(
        long telegramChannelId,
        string title,
        string caption,
        bool requiresApproval,
        DateTime createdAt,
        long? contentItemId = null,
        long? novelEpisodeId = null,
        string? imageUrl = null,
        DateTime? scheduledFor = null)
    {
        return new PublishQueueItem
        {
            TelegramChannelId = telegramChannelId,
            ContentItemId = contentItemId,
            NovelEpisodeId = novelEpisodeId,
            Title = title,
            Caption = caption,
            ImageUrl = imageUrl,
            ScheduledFor = scheduledFor,
            RequiresApproval = requiresApproval,
            Status = requiresApproval ? PublishStatus.WaitingForApproval : PublishStatus.Pending,
            CreatedAt = createdAt
        };
    }
}
