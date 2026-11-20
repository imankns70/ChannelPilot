using ChannelPilot.Domain.Common;

namespace ChannelPilot.Domain.Content;

public sealed class ContentSource : AuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public string? BaseUrl { get; set; }

    public string SourceKind { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public decimal ReliabilityScore { get; set; }

    public DateTime? LastFetchedAt { get; set; }

    public int FailureCount { get; set; }
}
