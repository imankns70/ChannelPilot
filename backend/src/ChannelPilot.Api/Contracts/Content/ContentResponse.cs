using ChannelPilot.Domain.Content;

namespace ChannelPilot.Api.Contracts.Content;

public sealed record ContentResponse(
    long Id,
    ContentType Type,
    ContentStatus Status,
    string OriginalTitle,
    string? OriginalSummary,
    string? SourceUrl,
    string? ImageUrl,
    string? SourceName,
    DateTime CollectedAt,
    decimal? FinalScore);
