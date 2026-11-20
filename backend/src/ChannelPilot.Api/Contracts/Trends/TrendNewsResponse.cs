using ChannelPilot.Domain.Trends;

namespace ChannelPilot.Api.Contracts.Trends;

public sealed record TrendNewsResponse(
    long Id,
    long ContentItemId,
    string Title,
    TrendCategory Category,
    decimal TrendScore,
    bool IsViral,
    bool IsSensitive,
    bool RequiresManualReview,
    string? Reason);
