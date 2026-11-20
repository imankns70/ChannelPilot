namespace ChannelPilot.Api.Contracts.Novels;

public sealed record ImportNovelRequest(
    string Title,
    string Text,
    string? Author,
    string? Description,
    string? Genre);
