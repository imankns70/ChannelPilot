namespace ChannelPilot.Api.Contracts.Novels;

public sealed record CreateNovelPublishingPlanRequest(
    long TelegramChannelId,
    DateOnly StartDate,
    TimeOnly PublishTime,
    int CurrentEpisodeNumber,
    bool IsActive);
