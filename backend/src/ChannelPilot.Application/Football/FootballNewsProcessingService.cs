using ChannelPilot.Domain.Content;
using ChannelPilot.Domain.Football;

namespace ChannelPilot.Application.Football;

public sealed class FootballNewsProcessingService
{
    public FootballNewsItem CreateInitialAnalysis(ContentItem content)
    {
        return new FootballNewsItem
        {
            ContentItemId = content.Id,
            Category = FootballNewsCategory.General,
            ImportanceLevel = FootballImportanceLevel.Medium,
            Reason = "Initial football analysis placeholder."
        };
    }
}
