namespace ChannelPilot.Application.DTOs.Publishing;

public sealed record PublishResultDto(
    bool IsSuccess,
    long? TelegramMessageId,
    string? Error);
