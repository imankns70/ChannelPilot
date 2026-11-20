using ChannelPilot.Domain.Content;

namespace ChannelPilot.Application.Football;

public sealed class FootballCaptionService
{
    public string Build(ContentItem content)
    {
        return string.IsNullOrWhiteSpace(content.OriginalSummary)
            ? content.OriginalTitle
            : $"{content.OriginalTitle}\n\n{content.OriginalSummary}";
    }
}
