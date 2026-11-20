namespace ChannelPilot.Api.Contracts.Trends;

public sealed record ReviewTrendRequest(bool Approved, string? Reason);
