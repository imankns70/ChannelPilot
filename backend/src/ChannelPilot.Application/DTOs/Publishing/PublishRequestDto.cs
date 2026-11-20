namespace ChannelPilot.Application.DTOs.Publishing;

public sealed record PublishRequestDto(
    long TelegramChatId,
    string Title,
    string Caption,
    string? ImageUrl);
