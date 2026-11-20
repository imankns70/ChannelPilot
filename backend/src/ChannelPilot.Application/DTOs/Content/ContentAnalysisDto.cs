using ChannelPilot.Domain.Content;

namespace ChannelPilot.Application.DTOs.Content;

public sealed record ContentAnalysisDto(
    ContentType ContentType,
    ContentQualityScore QualityScore,
    bool IsSensitive,
    string? Reason);
