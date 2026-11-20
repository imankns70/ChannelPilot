using ChannelPilot.Domain.Channels;
using ChannelPilot.Domain.Common;
using ChannelPilot.Domain.Content;

namespace ChannelPilot.Application.Channels;

public sealed class ChannelPolicyService
{
    public Result CheckContentPolicy(TelegramChannel channel, ContentItem content)
    {
        if (channel.Status != ChannelStatus.Active)
        {
            return Result.Failure("Channel is not active.");
        }

        if (content.QualityScore is not null &&
            content.QualityScore.FinalScore < channel.MinScoreToPublish)
        {
            return Result.Failure("Content quality score is below the channel minimum.");
        }

        return Result.Success();
    }

    public bool RequiresApproval(TelegramChannel channel) => channel.RequiresApproval;
}
