namespace ChannelPilot.Api.Contracts.Novels;

public sealed record NovelEpisodeResponse(
    long Id,
    long NovelId,
    int EpisodeNumber,
    string Title,
    string Content,
    string? HookText,
    string? EndingTeaser,
    bool IsPublished,
    DateTime? PublishedAt);
