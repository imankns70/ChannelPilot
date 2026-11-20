namespace ChannelPilot.Api.Contracts.Novels;

public sealed record NovelPublishingPlanResponse(
    long NovelId,
    long TelegramChannelId,
    DateOnly StartDate,
    TimeOnly PublishTime,
    int CurrentEpisodeNumber,
    bool IsActive,
    bool IsPlaceholder);
