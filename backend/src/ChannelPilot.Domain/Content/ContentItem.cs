using ChannelPilot.Domain.Common;

namespace ChannelPilot.Domain.Content;

public sealed class ContentItem : AuditableEntity
{
    public long? SourceId { get; set; }

    public ContentType Type { get; set; }

    public ContentStatus Status { get; set; }

    public string OriginalTitle { get; set; } = string.Empty;

    public string? OriginalSummary { get; set; }

    public string? OriginalBody { get; set; }

    public string? SourceUrl { get; set; }

    public string? ImageUrl { get; set; }

    public string? SourceName { get; set; }

    public DateTime? PublishedAt { get; set; }

    public DateTime CollectedAt { get; set; }

    public string? NormalizedTitle { get; set; }

    public string? ContentHash { get; set; }

    public ContentQualityScore? QualityScore { get; set; }
}
