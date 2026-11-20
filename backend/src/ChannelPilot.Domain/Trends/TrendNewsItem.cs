using ChannelPilot.Domain.Common;

namespace ChannelPilot.Domain.Trends;

public sealed class TrendNewsItem : Entity
{
    public long ContentItemId { get; set; }

    public TrendCategory Category { get; set; }

    public decimal TrendScore { get; set; }

    public bool IsViral { get; set; }

    public bool IsSensitive { get; set; }

    public bool RequiresManualReview { get; set; }

    public string? Reason { get; set; }
}
