namespace ChannelPilot.Domain.Channels;

public sealed class PublishingPolicy
{
    public bool RequiresApproval { get; set; }

    public decimal MinScoreToPublish { get; set; }

    public int MaxPostsPerDay { get; set; }

    public int MinMinutesBetweenPosts { get; set; }

    public TimeOnly? FixedPublishTime { get; set; }
}
