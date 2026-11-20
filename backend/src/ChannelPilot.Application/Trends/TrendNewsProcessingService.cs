using ChannelPilot.Domain.Content;
using ChannelPilot.Domain.Trends;

namespace ChannelPilot.Application.Trends;

public sealed class TrendNewsProcessingService
{
    public TrendNewsItem CreateInitialAnalysis(ContentItem content)
    {
        return new TrendNewsItem
        {
            ContentItemId = content.Id,
            Category = TrendCategory.General,
            RequiresManualReview = true,
            Reason = "Initial trend analysis placeholder."
        };
    }
}
