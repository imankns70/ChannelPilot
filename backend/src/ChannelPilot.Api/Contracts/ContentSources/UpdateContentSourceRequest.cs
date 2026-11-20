using System.ComponentModel.DataAnnotations;

namespace ChannelPilot.Api.Contracts.ContentSources;

public sealed record UpdateContentSourceRequest(
    [Required, MaxLength(200)] string Name,
    [MaxLength(2_000)] string? BaseUrl,
    [Required, MaxLength(50)] string SourceKind,
    bool IsActive,
    [Range(typeof(decimal), "0", "100")] decimal ReliabilityScore,
    DateTime? LastFetchedAt,
    [Range(0, int.MaxValue)] int FailureCount);
