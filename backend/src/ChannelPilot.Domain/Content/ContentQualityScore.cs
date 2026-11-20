namespace ChannelPilot.Domain.Content;

public sealed class ContentQualityScore
{
    public decimal ImportanceScore { get; set; }

    public decimal AttractivenessScore { get; set; }

    public decimal FreshnessScore { get; set; }

    public decimal ReliabilityScore { get; set; }

    public decimal FinalScore { get; set; }
}
