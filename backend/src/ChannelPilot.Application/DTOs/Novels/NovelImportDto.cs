namespace ChannelPilot.Application.DTOs.Novels;

public sealed record NovelImportDto(
    string Title,
    string Text,
    string? Author,
    string? Description,
    string? Genre);
