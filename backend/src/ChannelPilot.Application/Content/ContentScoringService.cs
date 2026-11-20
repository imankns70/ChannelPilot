using ChannelPilot.Domain.Content;

namespace ChannelPilot.Application.Content;

public sealed class ContentScoringService
{
    public ContentQualityScore Calculate(
        decimal importanceScore,
        decimal attractivenessScore,
        decimal freshnessScore,
        decimal reliabilityScore)
    {
        return new ContentQualityScore
        {
            ImportanceScore = importanceScore,
            AttractivenessScore = attractivenessScore,
            FreshnessScore = freshnessScore,
            ReliabilityScore = reliabilityScore,
            FinalScore = (importanceScore + attractivenessScore + freshnessScore + reliabilityScore) / 4m
        };
    }
}
