namespace ChannelPilot.Application.DTOs.Content;

public sealed record ContentRewriteDto(
    string Title,
    string Caption,
    string? ImageUrl);
