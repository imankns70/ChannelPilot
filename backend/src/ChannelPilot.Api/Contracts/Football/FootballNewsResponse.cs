using ChannelPilot.Domain.Football;

namespace ChannelPilot.Api.Contracts.Football;

public sealed record FootballNewsResponse(
    long Id,
    long ContentItemId,
    string Title,
    FootballNewsCategory Category,
    FootballImportanceLevel ImportanceLevel,
    string? MainEntity,
    bool IsTransferRelated,
    bool IsBreakingNews,
    bool IsSensitive,
    string? Reason);
