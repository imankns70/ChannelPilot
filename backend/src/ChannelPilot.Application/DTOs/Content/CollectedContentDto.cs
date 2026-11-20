namespace ChannelPilot.Application.DTOs.Content;

public sealed record CollectedContentDto(
    string Title,
    string? Summary,
    string? Body,
    string? SourceUrl,
    string? ImageUrl,
    string? SourceName,
    DateTime CollectedAt);
