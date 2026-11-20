using ChannelPilot.Application.Abstractions.Ai;
using ChannelPilot.Application.DTOs.Content;
using ChannelPilot.Domain.Content;

namespace ChannelPilot.Infrastructure.AI;

public sealed class FakeAiContentAnalyzer : IAiContentAnalyzer
{
    public Task<ContentAnalysisDto> AnalyzeAsync(
        ContentItem content,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var score = new ContentQualityScore();
        return Task.FromResult(new ContentAnalysisDto(
            content.Type,
            score,
            false,
            "Fake analysis placeholder."));
    }
}
