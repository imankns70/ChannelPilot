using ChannelPilot.Domain.Channels;
using ChannelPilot.Domain.Common;

namespace ChannelPilot.Application.Publishing;

public sealed class PublishingLimiter
{
    public Result Check(
        TelegramChannel channel,
        int postsPublishedToday,
        DateTime? lastPublishedAt,
        DateTime currentTime)
    {
        if (channel.MaxPostsPerDay > 0 && postsPublishedToday >= channel.MaxPostsPerDay)
        {
            return Result.Failure("The daily publishing limit has been reached.");
        }

        if (lastPublishedAt.HasValue &&
            currentTime < lastPublishedAt.Value.AddMinutes(channel.MinMinutesBetweenPosts))
        {
            return Result.Failure("The minimum interval between posts has not elapsed.");
        }

        if (channel.Type == ChannelType.Novel &&
            channel.FixedPublishTime.HasValue &&
            TimeOnly.FromDateTime(currentTime) < channel.FixedPublishTime.Value)
        {
            return Result.Failure("The fixed novel publishing time has not arrived.");
        }

        return Result.Success();
    }
}
