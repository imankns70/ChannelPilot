using ChannelPilot.Domain.Novels;

namespace ChannelPilot.Api.Contracts.Novels;

public sealed record NovelResponse(
    long Id,
    string Title,
    string? Author,
    string? Description,
    string? Genre,
    NovelStatus Status,
    int TotalEpisodes);
