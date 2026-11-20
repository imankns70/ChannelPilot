namespace ChannelPilot.Application.DTOs.Novels;

public sealed record NovelEpisodeDraftDto(
    int EpisodeNumber,
    string Title,
    string Content,
    string? EndingTeaser);
