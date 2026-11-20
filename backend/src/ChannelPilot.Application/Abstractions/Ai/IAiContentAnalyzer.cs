using ChannelPilot.Application.DTOs.Content;
using ChannelPilot.Domain.Content;

namespace ChannelPilot.Application.Abstractions.Ai;

public interface IAiContentAnalyzer
{
    Task<ContentAnalysisDto> AnalyzeAsync(
        ContentItem content,
        CancellationToken cancellationToken = default);
}
