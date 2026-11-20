using ChannelPilot.Domain.Novels;

namespace ChannelPilot.Application.Novels;

public sealed class NovelPublishingScheduler
{
    public bool IsDue(
        NovelPublishingPlan plan,
        DateOnly currentDate,
        TimeOnly currentTime,
        bool hasPublishedEpisodeToday)
    {
        return plan.IsActive &&
               currentDate >= plan.StartDate &&
               currentTime >= plan.PublishTime &&
               !hasPublishedEpisodeToday;
    }
}
