using ChannelPilot.Domain.Channels;

namespace ChannelPilot.Api.Contracts.Channels;

public sealed record CreateChannelRequest(
    string Name,
    long TelegramChatId,
    string? Username,
    ChannelType Type,
    bool RequiresApproval,
    decimal MinScoreToPublish,
    int MaxPostsPerDay,
    int MinMinutesBetweenPosts,
    TimeOnly? FixedPublishTime);
