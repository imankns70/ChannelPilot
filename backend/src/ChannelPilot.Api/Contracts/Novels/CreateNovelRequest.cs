namespace ChannelPilot.Api.Contracts.Novels;

public sealed record CreateNovelRequest(
    string Title,
    string? Author,
    string? Description,
    string? Genre);
