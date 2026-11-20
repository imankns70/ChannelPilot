using ChannelPilot.Domain.Common;

namespace ChannelPilot.Domain.Novels;

public sealed class NovelPublishingPlan : Entity
{
    public long NovelId { get; set; }

    public long TelegramChannelId { get; set; }

    public DateOnly StartDate { get; set; }

    public TimeOnly PublishTime { get; set; }

    public int CurrentEpisodeNumber { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastPublishedAt { get; set; }
}
