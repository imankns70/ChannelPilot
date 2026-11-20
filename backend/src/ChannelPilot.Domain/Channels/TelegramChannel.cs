using ChannelPilot.Domain.Common;

namespace ChannelPilot.Domain.Channels;

public sealed class TelegramChannel : AuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public long TelegramChatId { get; set; }

    public string? Username { get; set; }

    public ChannelType Type { get; set; }

    public ChannelStatus Status { get; set; }

    public bool RequiresApproval { get; set; }

    public decimal MinScoreToPublish { get; set; }

    public int MaxPostsPerDay { get; set; }

    public int MinMinutesBetweenPosts { get; set; }

    public TimeOnly? FixedPublishTime { get; set; }
}
