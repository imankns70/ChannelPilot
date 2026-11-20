namespace ChannelPilot.Api.Contracts.ContentSources;

public sealed record ContentSourceResponse(
    long Id,
    string Name,
    string? BaseUrl,
    string SourceKind,
    bool IsActive,
    decimal ReliabilityScore,
    DateTime? LastFetchedAt,
    int FailureCount,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
