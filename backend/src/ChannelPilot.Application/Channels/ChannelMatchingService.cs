using ChannelPilot.Application.DTOs.Channels;
using ChannelPilot.Domain.Channels;
using ChannelPilot.Domain.Content;

namespace ChannelPilot.Application.Channels;

public sealed class ChannelMatchingService
{
    public ChannelMatchDto Match(ContentItem content, TelegramChannel channel)
    {
        if (channel.Status != ChannelStatus.Active)
        {
            return new(false, "Channel is not active.");
        }

        var isMatch = (content.Type, channel.Type) switch
        {
            (ContentType.FootballNews, ChannelType.Football) => true,
            (ContentType.TrendNews, ChannelType.Trend) => true,
            (ContentType.NovelEpisode, ChannelType.Novel) => true,
            _ => false
        };

        return isMatch
            ? new(true, null)
            : new(false, "Content type does not match the channel type.");
    }
}
