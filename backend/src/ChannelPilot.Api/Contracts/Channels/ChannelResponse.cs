using ChannelPilot.Domain.Channels;

namespace ChannelPilot.Api.Contracts.Channels;

public sealed record ChannelResponse(
    long Id,
    string Name,
    long TelegramChatId,
    string? Username,
    ChannelType Type,
    ChannelStatus Status,
    bool RequiresApproval,
    decimal MinScoreToPublish,
    int MaxPostsPerDay,
    int MinMinutesBetweenPosts,
    TimeOnly? FixedPublishTime);
