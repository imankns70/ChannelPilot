namespace ChannelPilot.Api.Contracts.Publishing;

public sealed record PublishNowRequest(bool IgnoreSchedule = false);
